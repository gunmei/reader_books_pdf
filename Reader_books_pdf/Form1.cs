using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PdfiumViewer;

namespace Reader_books_pdf
{
    public partial class HeaderForm : Form
    {
        private SearchForm searchForm;

        public HeaderForm()
        {
            try
            {   
                InitializeComponent();
                
                pdfViewer1.Renderer.DisplayRectangleChanged += Renderer_DisplayRectangleChanged;
                pdfViewer1.Renderer.ZoomChanged += Renderer_ZoomChanged;

                pdfViewer1.Renderer.MouseMove += Renderer_MouseMove;
                pdfViewer1.Renderer.MouseLeave += Renderer_MouseLeave;
                ShowPdfLocation(PdfPoint.Empty);

                cutMarginsWhenPrintingToolStripMenuItem.PerformClick();

                zoom.Text = pdfViewer1.Renderer.Zoom.ToString();

                Disposed += (s, e) => pdfViewer1.Document?.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Что-то пошло не так. Приносим свои извенения. Свяжитесь с разработчиком по номеру +79966286382. Текст ошибки: " + ex.Message); ;
            }
        }

        #region Обработка вкладки - Файл
        //Метод обработки и выборки файла из доалогового окна
        private void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
            dialog.RestoreDirectory = true;
            dialog.Title = "Open PDF File";

            if(dialog.ShowDialog(this) != DialogResult.OK)
            {
                //Восстанавливаем значения по умолчанию
                dialog.Reset();
            }
            else
            {
                pdfViewer1.Document?.Dispose();
                pdfViewer1.Document = OpenFilePDF(dialog.FileName);
            }

           
        }

        //Обработка открытия pdf документа
        public PdfDocument OpenFilePDF(string filepath)
        {
            try
            {
                return PdfDocument.Load(this, filepath);
            }
            catch (Exception)
            {
                MessageBox.Show("Для работы необходимо выбрать .pdf файл. Возможно по указаному пути больше нет файла с указанным именем.");
                return null;
            }
        }

        //Открытие диалогового окна
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        //Закрытие приложения
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Предварительный просмотр печати
        private void PrintPreviewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var form = new PrintPreviewDialog()) // Равноценно записи вида PrintPreviewDialog form = new PrintPreviewDialog();
            {
                form.Document = pdfViewer1.Document.CreatePrintDocument(pdfViewer1.DefaultPrintMode);
                form.ShowDialog(this);
            }
        }


        // Печать нескольких страниц
        private void PrintMultiplePagesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var form = new PrintMultiplePagesForm(pdfViewer1))
            {
                form.ShowDialog(this);
            }
        }

        #endregion

        #region Обработка вкладки - Инструменты
        //Открытие формы поиска
        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(searchForm == null)
            {
                searchForm = new SearchForm(pdfViewer1.Renderer);
                searchForm.Disposed += (s, ea) => searchForm = null;
                searchForm.Show(this);
            }

            searchForm.Focus();
        }

        // Рендринг в растровое изображение
        private void renderToBitmapsStripMenuItem_Click(object sender, EventArgs e)
        {
            int dpiX;
            int dpiY;


            //Определяем качество для растрированого изображения

            using (var form = new Export_bitmaps())
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                dpiX = form.DpiX;
                dpiY = form.DpiY;
            }


            //Путь куда будут сохранятся растрированые изображения
            string path;

            using (var form = new FolderBrowserDialog())
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                path = form.SelectedPath;
            }

            //Цикл для расстрирования наименования растрированых изображений
            var document = pdfViewer1.Document;

            for (int i = 0; i < document.PageCount; i++)
            {
                using (var image = document.Render(i, (int)document.PageSizes[i].Width, (int)document.PageSizes[i].Height, dpiX, dpiY, false))
                {
                    image.Save(Path.Combine(path, "Page " + i + ".png"));
                }
            }
        }

        //Сокращайте поля при печати
        private void cutMarginsWhenPrintingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutMarginsWhenPrintingToolStripMenuItem.Checked = true;
            shrinkToMarginsWhenPrintingToolStripMenuItem.Checked = false;

            pdfViewer1.DefaultPrintMode = PdfPrintMode.CutMargin;
        }

        //Сокращение полей при печати
        private void shrinkToMarginsWhenPrintingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shrinkToMarginsWhenPrintingToolStripMenuItem.Checked = true;
            cutMarginsWhenPrintingToolStripMenuItem.Checked = false;

            pdfViewer1.DefaultPrintMode = PdfPrintMode.ShrinkToMargin;
        }

        //Удаление страницы в документе
        private void deleteCurrentPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // PdfRenderer не поддерживает изменения в уже загруженном документе,
            // поэтому мы удаляем страницу и перезагружаем документ в PdfRender.

            int page = pdfViewer1.Renderer.Page;
            var document = pdfViewer1.Document;
            pdfViewer1.Document = null;
            document.DeletePage(page);
            pdfViewer1.Document = document;
            pdfViewer1.Renderer.Page = page;
        }

        //Переворот текущей страницы(всего документа)
        #region 
        private void rotate0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rotate(PdfRotation.Rotate0);
        }
        private void rotate90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rotate(PdfRotation.Rotate90);
        }

        private void rotate180ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rotate(PdfRotation.Rotate180);
        }

        private void rotate270ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rotate(PdfRotation.Rotate270);
        }

        private void Rotate(PdfRotation rotate)
        {
            // PdfRenderer не поддерживает изменения в уже загруженном документе,
            // поэтому мы удаляем страницу и перезагружаем документ в PdfRender.

            int pageDopolnitelino = pdfViewer1.Renderer.Page;
            var document = pdfViewer1.Document;
            pdfViewer1.Document = null;
            document.RotatePage(pageDopolnitelino, rotate);
            pdfViewer1.Document = document;
            pdfViewer1.Renderer.Page = pageDopolnitelino;
        }


        #endregion

        //Отображения введенного диапазона страниц
        private void showRangeOfPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new RangePage(pdfViewer1.Document))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    pdfViewer1.Document = form.Document;
                }
            }
        }

        //Информация о документе
        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PdfInformation info = pdfViewer1.Document.GetInformation();
            StringBuilder sz = new StringBuilder();
            sz.AppendLine($"Author: {info.Author}");
            sz.AppendLine($"Creator: {info.Creator}");
            sz.AppendLine($"Keywords: {info.Keywords}");
            sz.AppendLine($"Producer: {info.Producer}");
            sz.AppendLine($"Subject: {info.Subject}");
            sz.AppendLine($"Title: {info.Title}");
            sz.AppendLine($"Create Date: {info.CreationDate}");
            sz.AppendLine($"Modified Date: {info.ModificationDate}");

            MessageBox.Show(sz.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Обрботка навигационной панели

        #region Отслеживание курсора мышки
        private void Renderer_MouseLeave(object sender, EventArgs e)
        {
            ShowPdfLocation(PdfPoint.Empty);
        }

        private void Renderer_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                ShowPdfLocation(pdfViewer1.Renderer.PointToPdf(e.Location));
            }
            catch(Exception ex)
            {
                MessageBox.Show("Текст ошибки: " + ex);
            }
            
        }

        private void ShowPdfLocation(PdfPoint point)
        {
            if (!point.IsValid)
            {
                pageToolStripLabel.Text = null;
                coordinatesToolStripLabel.Text = null;
            }
            else
            {
                pageToolStripLabel.Text = (point.Page + 1).ToString();
                coordinatesToolStripLabel.Text = point.Location.X + "," + point.Location.Y;
            }
        }
        #endregion

        //Ввод масштаба в текстовое поле
        void Renderer_ZoomChanged(object sender, EventArgs e)
        {
            zoom.Text = pdfViewer1.Renderer.Zoom.ToString();
        }

        //Ввод порядкового номера текушей страницы
        void Renderer_DisplayRectangleChanged(object sender, EventArgs e)
        {
            page.Text = (pdfViewer1.Renderer.Page + 1).ToString();
        }

        //Переход к предыдущей странице
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            pdfViewer1.Renderer.Page--;
        }

        //Переход к следующей странице
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            pdfViewer1.Renderer.Page++;
        }

        #region Масштабирование окна по высоте, по ширине, и чтобы вмещался в окно
        private void fitWidth_Click(object sender, EventArgs e)
        {
            FitPage(PdfViewerZoomMode.FitWidth);
        }

        private void FitPage(PdfViewerZoomMode zoomMode)
        {
            int page = pdfViewer1.Renderer.Page;
            pdfViewer1.ZoomMode = zoomMode;
            pdfViewer1.Renderer.Zoom = 1;
            pdfViewer1.Renderer.Page = page;
        }

        private void fitBest_Click(object sender, EventArgs e)
        {
            FitPage(PdfViewerZoomMode.FitBest);
        }

        private void fitHeight_Click(object sender, EventArgs e)
        {
            FitPage(PdfViewerZoomMode.FitHeight);
        }
        #endregion

        //Поле для ввода страницы
        private void page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                int pageDop;
                if (int.TryParse(page.Text, out pageDop))
                    pdfViewer1.Renderer.Page = pageDop - 1;
            }
        }

        //Поле для ввода масштаба
        private void zoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                float zoomDop;
                if (float.TryParse(zoom.Text, out zoomDop))
                    pdfViewer1.Renderer.Zoom = zoomDop;
            }
        }

        //Кнопка для увеличения масштаба
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            pdfViewer1.Renderer.ZoomIn();
        }

        //Кнопка для уменьшения масштаба
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            pdfViewer1.Renderer.ZoomOut();
        }

        //Кнопка для поворота страницы влево
        private void rotateLeft_Click(object sender, EventArgs e)
        {
            pdfViewer1.Renderer.RotateLeft();
        }

        //Кнопка для поворота страницы вправо
        private void rotateRight_Click(object sender, EventArgs e)
        {
            pdfViewer1.Renderer.RotateRight();
        }

        //Кнопка для скрытия или показа меню которое находится на 3-м уровне
        private void hideToolbar_Click(object sender, EventArgs e)
        {
            pdfViewer1.ShowToolbar = showToolbar.Checked;
        }
        
        //Кнопка для скрытия или показа панели навигации по закладкам в открытом документе
        private void hideBookmarks_Click(object sender, EventArgs e)
        {
            pdfViewer1.ShowBookmarks = showBookmarks.Checked;
        }

        //Кнопка для показа первых 125 сиволов в начале страницы и в конце страницы
        private void getTextFromPage_Click(object sender, EventArgs e)
        {
            int page = pdfViewer1.Renderer.Page;
            string text = pdfViewer1.Document.GetPdfText(page);
            string caption = string.Format("Page {0} contains {1} character(s):", page + 1, text.Length);

            if (text.Length > 128) text = text.Substring(0, 125) + "...\n\n\n\n..." + text.Substring(text.Length - 125);
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        //Открытие документа из другой формы
        public void OpenFileFromDataGridView(string path)
        {
            try
            {
                pdfViewer1.Document?.Dispose();
                pdfViewer1.Document = OpenFilePDF(path);
            }
            catch(Exception)
            {
                MessageBox.Show("Файл не найден.");
            }
            
        }
    }
}

