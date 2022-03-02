using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
//using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace Reader_books_pdf
{
    public partial class Welcome : Form
    {
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data source=read_book.mdb; Persist Security Info=False;";

        //string connectionString = "host=localhost;user=root;database=read_book;password=:5ginza()hou;";

        public Welcome()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
            textBox9.UseSystemPasswordChar = true;
        }


        //Регистрация
        private void button4_Click(object sender, EventArgs e)
        {
            //Подключение к бд
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    //вытаскиваем данные из полей
                    string name = textBox3.Text;
                    string lastname = textBox4.Text;
                    string surname = textBox5.Text;
                    string phone = textBox6.Text;
                    string email =  textBox7.Text;
                    string login = textBox8.Text;
                    string pass = GetHash(textBox9.Text);

                    //проверка на заполненность всех данных
                    if (login == "" || pass == "" || name == "" || lastname == "" || surname == "" || email == "" || phone == "")
                    {
                        MessageBox.Show("Необходимо заполнить все поля.");
                        return;
                    }
                    if (!isValid(email))
                    {
                        MessageBox.Show("Адресс email не прошел проверку валидности. Введите корректный адрес.");
                        return;
                    }

                    conn.Open();
                    //проверяем наличие такого пользователя в бд
                    string query = "SELECT * FROM users WHERE login = '" + login + "' AND pass = '" + pass + "'";
                    var accessCheckCommand = new OleDbCommand(query, conn);

                    // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT

                    OleDbDataReader reader = accessCheckCommand.ExecuteReader();
                    reader.Read();
                    //reader.Close();
                    if (!reader.HasRows)
                    {
                        //Регистрируем
                        query = "INSERT INTO `read_book`.`users` (`name`, `lastname`, `surname`, `number`, `Email`, `login`, `pass`) VALUES ('" + name + "','" + lastname + "','" + surname + "','" + phone + "','" + email + "','" + login + "','" + pass + "')";

                        var accessRegCommand = new OleDbCommand(query, conn);
                        accessRegCommand.ExecuteNonQuery();

                        MessageBox.Show("Вы успешно зарегистрированы!");
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                        textBox7.Clear();
                        textBox8.Clear();
                        textBox9.Clear();

                        //query = "SELECT * FROM users WHERE login = '" + login + "' AND pass = '" + pass + "'";
                        //accessCheckCommand = new OleDbCommand(query, conn);

                        //// получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
                        //reader = accessCheckCommand.ExecuteReader();
                        reader.Close();
                        conn.Close();
                        registration.Visible = false;
                        panel1.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким логином и паролем уже существует!");
                        reader.Close();
                        conn.Close();
                    }
                    
                }
                catch (Exception ex)
                {
                    // обработка исключения
                    MessageBox.Show("Возникла ошибка. Текст ошибки: " + ex.Message);
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            
        }

        bool isValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            registration.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            registration.Visible = true;
            panel1.Visible = false;
        }

        //Авторизация
        private void button1_Click(object sender, EventArgs e)
        {
            //Подключение к бд
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {   
                    conn.Open();
                    //вытаскиваем данные из полей
                    string login = textBox1.Text;
                    string pass = GetHash(textBox2.Text);

                    //проверка на заполненность всех данных
                    if (login == "" && pass == "")
                    {
                        MessageBox.Show("Заполните все поля");
                        conn.Close();
                        return;
                    }
                    //проверяем наличие такого пользователя в бд
                    string query = "SELECT * FROM users WHERE login = '" + login + "' AND pass = '" + pass + "'";
                    var accessCheckCommand = new OleDbCommand(query, conn);

                    // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
                    OleDbDataReader reader = accessCheckCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        MessageBox.Show("Здравствуйте, " + reader["name"].ToString() + " !");
                        
                        Choise work_form = new Choise();
                        reader.Close();
                        conn.Close();
                        work_form.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Такой пользователь не найден!");
                        conn.Close();
                        reader.Close();
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла ошибка. Текст ошибки: " + ex.Message);
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VkAuthorization vkauth = new VkAuthorization();
            vkauth.Show();
            this.Hide();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsLetter(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SeeOrNotSee(textBox2);
        }


        //Метод показать или закрыть пароль
        private bool SeeOrNotSee(TextBox textBox)
        {
            if (textBox.UseSystemPasswordChar == true)
            {
                return textBox.UseSystemPasswordChar = false;
            }
            else
            {
                return textBox.UseSystemPasswordChar = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SeeOrNotSee(textBox9);
        }
    }
}

