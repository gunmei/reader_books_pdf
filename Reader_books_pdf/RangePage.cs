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
    public partial class RangePage : Form
    {
        private readonly IPdfDocument document;

        public IPdfDocument Document { get; private set; }

        public RangePage(IPdfDocument documentThis)
        {
            document = documentThis;
            InitializeComponent();

            startPage.Text = "1";
            endPage.Text = documentThis.PageCount.ToString();
        }


        private void acceptButton_Click(object sender, EventArgs e)
        {
            int startPageDop;
            int endPageDop;

            if (
                !int.TryParse(startPage.Text, out startPageDop) ||
                !int.TryParse(endPage.Text, out endPageDop) ||
                startPageDop < 1 ||
                endPageDop > document.PageCount ||
                startPageDop > endPageDop
            )
            {
                MessageBox.Show(this, "Не выявлены начальная/последняя страница.");
            }
            else
            {
                Document = PdfRangeDocument.FromDocument(document, startPageDop - 1, endPageDop - 1);

                DialogResult = DialogResult.OK;
            }
        }
    }
}
