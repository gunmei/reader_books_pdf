using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using HtmlAgilityPack;
using System.IO;

namespace Reader_books_pdf
{
    public partial class VkAuthorization : Form
    {
        public VkAuthorization()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Welcome wel = new Welcome();
            wel.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string pass = textBox2.Text;
            var auth = new VkAuth(login, pass);
            if(login!="" && pass != "")
            {
                switch (auth.CheckAuth())
                {
                    case 0:
                        MessageBox.Show("Введенны данные которых не существуют в базе. Пожалуйста перепроверьте введенные данные или зарегистрируйтесь");
                        break;
                    case 1:
                        MessageBox.Show("Вы успешно авторизовались.");
                        Choise work = new Choise();
                        work.Show();
                        this.Close();
                        Unset(login);
                        Unset(pass);
                        break;
                    case 2:
                        MessageBox.Show("У вас двухуровневая авторизация в ВК. К сожеланию авторизоваться в приложении через ВК не удасться. Пожалуйста пройдите регистрацию.");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.");
            }
            
        }

        private string Unset(string row)
        {
            string rows = row;
            return rows;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная авторизация проверяет существование вашей личности на осонове реальности вашей страничке в социальной сети Вконтакте. При этом никакие данные не сохраняются, после успешной авторизации удаляются.");
        }
    }
}
