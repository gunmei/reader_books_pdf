using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Data.OleDb;

namespace Reader_books_pdf
{
    public partial class Choise : Form
    {

        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data source=read_book.mdb; Persist Security Info=False;";
        //string connectionString = "host=localhost;user=root;database=read_book;password=:5ginza()hou;";

        public Choise()
        {
            InitializeComponent();

            using(OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM read_book.list_open_book";
                    OleDbCommand command = new OleDbCommand(query, conn);
                    OleDbDataReader reader = command.ExecuteReader();
                    List<string[]> data = new List<string[]>();
                    while (reader.Read())
                    {
                        data.Add(new string[4]);
                        data[data.Count - 1][0] = reader[0].ToString();
                        data[data.Count - 1][1] = reader[1].ToString();
                        data[data.Count - 1][2] = reader[2].ToString();
                        data[data.Count - 1][3] = reader[3].ToString();
                    }
                    reader.Close();

                    foreach (string[] s in data)
                    {
                        dataGridView1.Rows.Add(s);
                    }
                    conn.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Возникла ошибка. Текст ошибки: " + ex.Message);
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();

                    }
                }
                


                //conn.Open();
                //string query1 = "SELECT * FROM read_book.list_open_book WHERE `idlist_open_book`=8";
                //OleDbCommand command1 = new OleDbCommand(query1, conn);
                //OleDbDataReader reader1 = command1.ExecuteReader();
                //reader1.Read();
                ////textBox2.Text = reader1["path_file"].ToString();
                //textBox3.Text = GetPathDecod(reader1["path_file"].ToString());
                //reader1.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                dialog.RestoreDirectory = true;
                dialog.Title = "Open PDF File";
                
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    
                }
                DateTime ThToday = DateTime.Now;
                string nameFile = dialog.SafeFileName;
                string pathFile = dialog.FileName;
                pathFile = pathFile.Replace("\\", "/");
                //textBox2.Text = pathFile;
                if (nameFile != "" && pathFile != "")
                {
                    try
                    {
                        conn.Open();
                        string query = @"INSERT INTO `read_book`.`list_open_book` (`name_file`, `path_file`, `data_time`) VALUES ('" + nameFile + "', '" + pathFile + "', '" + ThToday.ToString() +"')";
                        var accessCommand = new OleDbCommand(query, conn);
                        accessCommand.ExecuteNonQuery();
                        //MessageBox.Show("Открытая книга успешно сохранена)");
                        conn.Close();
                        HeaderForm valuePath = new HeaderForm();
                        valuePath.OpenFileFromDataGridView(pathFile);
                        valuePath.Show();
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Возникла ошибка. Текст ошибки: " + ex.Message);
                        if (conn != null && conn.State == ConnectionState.Open)
                        {
                            conn.Close();

                        }
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string valueFromCell = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            if (File.Exists(valueFromCell))
            {
                //MessageBox.Show(valueFromCell);

                HeaderForm valuePath = new HeaderForm();
                valuePath.OpenFileFromDataGridView(valueFromCell);
                valuePath.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Извините, файл по указанному пути не найден.");
            }
        }
    }
}
