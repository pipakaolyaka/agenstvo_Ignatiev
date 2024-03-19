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

    public partial class registracia : Window
    {
        public registracia()
        {
            InitializeComponent();
        }

        private void registrB_Click(object sender, RoutedEventArgs e)
        {
            if (loginT.Text == "" || parolT.Text == "" || pochtaT.Text == "" || imyaT.Text == "" || familiyaT.Text == "" || nomer_telefonaT.Text == "")
            {
                MessageBox.Show("поля заполни");
            }
            else { 
                string conn = "data source = stud-mssql.sttec.yar.ru,38325; initial catalog =user232_db; user id =user232_db; password =user232; MultipleActiveResultSets = True; App = EntityFramework";
            string insertQuery = "INSERT INTO [Agenstvo_klient] (login, pass, imya,familiya,nomer_telefona,email) VALUES (@login, @pass,@imya,@familiya,@nomer_telefona,@email)";

                using (SqlConnection sqlConnection = new SqlConnection(conn))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(insertQuery, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.AddWithValue("@email", pochtaT.Text);
                        sqlCommand.Parameters.AddWithValue("@imya", imyaT.Text);
                        sqlCommand.Parameters.AddWithValue("@familiya", familiyaT.Text);
                        sqlCommand.Parameters.AddWithValue("@nomer_telefona", nomer_telefonaT.Text);
                        sqlCommand.Parameters.AddWithValue("@login", loginT.Text);
                        sqlCommand.Parameters.AddWithValue("@pass", parolT.Text);

                        int rowsAffected = sqlCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Регистрация успешно завершена.");
                            loginT.Text = "";
                            parolT.Text = "";
                            pochtaT.Text = "";
                            imyaT.Text = "";
                            familiyaT.Text = ""; 
                            nomer_telefonaT.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Произошла ошибка при регистрации.");
                        }
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
