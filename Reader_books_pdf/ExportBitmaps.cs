using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reader_books_pdf
{
    public partial class Export_bitmaps : Form
    {
        //Объявление дополнительных приватных переменных

        private int dpiX;
        private int dpiY;

        //Методы для возвращения значений приватных переменных
        public int DpiX
        {
            get { return dpiX; }
        }

        public int DpiY
        {
            get { return dpiX; }
        }

        public Export_bitmaps()
        {
            InitializeComponent();
            UpdateEnabled();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void dpiX_TextChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        private void dpiY_TextChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        private void UpdateEnabled()
        {
            acceptButton.Enabled =
                int.TryParse(dpiXTextBox.Text, out dpiX) &&
                int.TryParse(dpiYTextBox.Text, out dpiY);
        }

    }

}
