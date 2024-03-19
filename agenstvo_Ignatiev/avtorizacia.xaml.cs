using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace agenstvo_Ignatiev
{
    /// <summary>
    /// Логика взаимодействия для avtorizacia.xaml
    /// </summary>
    public partial class avtorizacia : Window
    {
        public avtorizacia()
        {
            InitializeComponent();
        }

        private void avtor_click(object sender, RoutedEventArgs e)
        {
            if (loginT.Text == "admin" || parolT.Text == "admin")
            {
                MessageBox.Show("Вы успешно вошли как администратор!");
                Hide();
                admin admin = new admin();
                admin.Show();
            }
            if (loginT.Text == "" || parolT.Text == "")
            {
                MessageBox.Show("поля заполни");
            }
            else
            {
                string connection = "data source=stud-mssql.sttec.yar.ru,38325;user id=user232_db;password=user232;";
                string command = "select login,pass from [Agenstvo_klient] where login=@login and pass=@pass";
                string login = "";
                string pass = "";

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@login", loginT.Text);
                        sqlCommand.Parameters.AddWithValue("@pass", parolT.Text);
                        sqlConnection.Open();
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                login = sqlDataReader.GetString(0);
                                pass = sqlDataReader.GetString(1);
                            }
                            if ((login == loginT.Text) && (pass == parolT.Text))
                            {
                                MessageBox.Show("Вы успешно вошли!");
                                glavnaya glavnaya = new glavnaya();
                                glavnaya.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неправильный логин или пароль!");
                            }
                        }
                        else { MessageBox.Show("Неправильный логин или пароль!"); }
                    }
                }
            }
        }

        private void vihodB_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
