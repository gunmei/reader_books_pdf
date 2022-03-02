using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfiumViewer;

namespace Reader_books_pdf
{
    public partial class PrintMultiplePagesForm : Form
    {
        private PdfViewer _viewer;
        public PrintMultiplePagesForm(PdfViewer viewer)
        {
            if (viewer == null)
                throw new ArgumentNullException(nameof(viewer));

            _viewer = viewer;
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            int horizontal_d, vertical_d;
            float margin_d;
            
            if(!int.TryParse(horizontal.Text, out horizontal_d))
            {
                MessageBox.Show(this, "");
            }
            else if(!int.TryParse(vertical.Text, out vertical_d))
            {
                MessageBox.Show(this, "");
            }
            else if(!float.TryParse(margin.Text, out margin_d))
            {
                MessageBox.Show(this, "");
            }
            else
            {
                var settings = new PdfPrintSettings(
                    _viewer.DefaultPrintMode,
                    new PdfPrintMultiplePages(
                        horizontal_d,
                        vertical_d,
                        horizontalOrientation.Checked ? Orientation.Horizontal : Orientation.Vertical,
                        margin_d
                    )
                );

                using (var form = new PrintPreviewDialog())
                {
                    form.Document = _viewer.Document.CreatePrintDocument(settings);
                    form.ShowDialog(this);
                }

                DialogResult = DialogResult.OK;
            }
        }
    }
}
