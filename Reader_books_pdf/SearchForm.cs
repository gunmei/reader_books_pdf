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
    public partial class SearchForm : Form
    {
        private readonly PdfSearchManager searchManager;
        private bool findDirty;

        public SearchForm(PdfRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException(nameof(renderer));

            InitializeComponent();

            searchManager = new PdfSearchManager(renderer);

            matchCase.Checked = searchManager.MatchCase;
            matchWholeWord.Checked = searchManager.MatchWholeWord;
            highlightAll.Checked = searchManager.HighlightAllMatches;
        }

        private void MatchCase_CheckedChanged(object sender, EventArgs e)
        {
            findDirty = true;
            searchManager.MatchCase = matchCase.Checked;
        }

        private void MatchWholeWord_CheckedChanged(object sender, EventArgs e)
        {
            findDirty = true;
            searchManager.MatchWholeWord = matchCase.Checked;
        }

        private void HighlightAll_CheckedChanged(object sender, EventArgs e)
        {
            searchManager.HighlightAllMatches = highlightAll.Checked;
        }

        private void find_TextChanged(object sender, EventArgs e)
        {
            findDirty = true;
        }

        private void findPrevious_Click(object sender, EventArgs e)
        {
            Find(false);
        }

        private void findNext_Click(object sender, EventArgs e)
        {
            Find(true);
        }

        private void Find(bool forward)
        {
            if (findDirty)
            {
                findDirty = false;

                if (!searchManager.Search(find.Text))
                {
                    MessageBox.Show(this, "Совпадений не найдно.");
                    return;
                }
            }

            if (!searchManager.FindNext(forward))
                MessageBox.Show(this, "Поиск достиг начальной точки поиск.");
        }
    }
}
