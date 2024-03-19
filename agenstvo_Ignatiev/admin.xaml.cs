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
using System.Data;


namespace agenstvo_Ignatiev
{
    /// <summary>
    /// Логика взаимодействия для admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        public admin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tipC.Items.Add("");
            komnatC.Items.Add("");
            dostupC.Items.Add("");


            string connectionString = "data source = stud-mssql.sttec.yar.ru,38325; initial catalog = user232_db; user id = user232_db; password = user232; MultipleActiveResultSets = True; App = EntityFramework";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT tip, adres, cena, ploshad, kolichestvo_komnat, opisanie, Dostupnost FROM [Agenstvo_nedvijimost]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    string privivkiQuery = "SELECT DISTINCT tip FROM [Agenstvo_nedvijimost]";
                    SqlCommand privivkiCommand = new SqlCommand(privivkiQuery, connection);
                    SqlDataReader reader = privivkiCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        tipC.Items.Add(reader["tip"].ToString());
                        tipC2.Items.Add(reader["tip"].ToString());
                    }
                    reader.Close();

                    string familiiQuery = "SELECT DISTINCT kolichestvo_komnat FROM [Agenstvo_nedvijimost]";
                    SqlCommand familiiCommand = new SqlCommand(familiiQuery, connection);
                    SqlDataReader reader1 = familiiCommand.ExecuteReader();
                    while (reader1.Read())
                    {
                        komnatC.Items.Add(reader1["kolichestvo_komnat"].ToString());
                        komnatC2.Items.Add(reader1["kolichestvo_komnat"].ToString());
                    }
                    reader1.Close();

                    string DostupnostQuery = "SELECT DISTINCT Dostupnost FROM [Agenstvo_nedvijimost]";
                    SqlCommand DostupnostCommand = new SqlCommand(DostupnostQuery, connection);
                    SqlDataReader reader2 = DostupnostCommand.ExecuteReader();
                    while (reader2.Read())
                    {
                        dostupC.Items.Add(reader2["Dostupnost"].ToString());
                        dostupC2.Items.Add(reader2["Dostupnost"].ToString());
                    }
                    reader2.Close();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.ItemsSource = dataTable.DefaultView;
                }
            }
        }

        private void vihodB_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void poiskB_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "data source = stud-mssql.sttec.yar.ru,38325; initial catalog = user232_db; user id = user232_db; password = user232; MultipleActiveResultSets = True; App = EntityFramework";

            string tip = tipC.SelectedItem?.ToString();
            string kolichestvo_komnat = komnatC.SelectedItem?.ToString();
            string Dostupnost = dostupC.SelectedItem?.ToString();

            string query = "SELECT tip, adres, cena, ploshad, kolichestvo_komnat, opisanie, Dostupnost FROM [Agenstvo_nedvijimost] WHERE 1 = 1";

            if (!string.IsNullOrEmpty(tip))
            {
                query += $" AND tip = '{tip}'";
            }

            if (!string.IsNullOrEmpty(kolichestvo_komnat))
            {
                query += $" AND kolichestvo_komnat = '{kolichestvo_komnat}'";
            }

            if (!string.IsNullOrEmpty(Dostupnost))
            {
                query += $" AND Dostupnost = '{Dostupnost}'";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.ItemsSource = dataTable.DefaultView;
                }
            }

        }
        private void dobavitB_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "data source = stud-mssql.sttec.yar.ru,38325; initial catalog = user232_db; user id = user232_db; password = user232; MultipleActiveResultSets = True; App = EntityFramework";

                string tip = tipC2.Text;
                string adres = adresT.Text;
                string cena = cenaT.Text;
                string ploshad = ploshadT.Text;
                string kolichestvo_komnat = komnatC2.Text;
                string opisanie = opisanieT.Text;
                string dostupnost = dostupC2.Text;

                if (!string.IsNullOrEmpty(tip) && !string.IsNullOrEmpty(adres) && !string.IsNullOrEmpty(cena) && !string.IsNullOrEmpty(ploshad) && !string.IsNullOrEmpty(kolichestvo_komnat) && !string.IsNullOrEmpty(opisanie) && !string.IsNullOrEmpty(dostupnost))
                {
                    string query = $"INSERT INTO [Agenstvo_nedvijimost] (tip, adres, cena, ploshad, kolichestvo_komnat, opisanie, Dostupnost) VALUES ('{tip}', '{adres}', '{cena}', '{ploshad}', '{kolichestvo_komnat}', '{opisanie}', '{dostupnost}')";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Информация успешно добавлена в базу данных.");
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                }
            }

        private void izmenitB_Click(object sender, RoutedEventArgs e)
        {
         
            if (dataGridView1.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dataGridView1.SelectedItem;

                // Получение значений выделенной строки
                string tip = selectedRow["tip"].ToString();
                string adres = selectedRow["adres"].ToString();
                string cena = selectedRow["cena"].ToString();
                string ploshad = selectedRow["ploshad"].ToString();
                string kolichestvo_komnat = selectedRow["kolichestvo_komnat"].ToString();
                string opisanie = selectedRow["opisanie"].ToString();
                string dostupnost = selectedRow["Dostupnost"].ToString();

                // Вывод значений в текстовые поля
                tipC2.Text = tip;
                adresT.Text = adres;
                cenaT.Text = cena;
                ploshadT.Text = ploshad;
                komnatC2.Text = kolichestvo_komnat;
                opisanieT.Text = opisanie;
                dostupC2.Text = dostupnost;

                // Добавление обработчика события для сохранения изменений при повторном нажатии на кнопку
                izmenitB.Click -= izmenitB_Click; // Удаление текущего обработчика
                izmenitB.Click += (s, ev) =>
                {
                    // Сохранение измененных данных обратно в выделенную строку
                    selectedRow["tip"] = tipC2.Text;
                    selectedRow["adres"] = adresT.Text;
                    selectedRow["cena"] = cenaT.Text;
                    selectedRow["ploshad"] = ploshadT.Text;
                    selectedRow["kolichestvo_komnat"] = komnatC2.Text;
                    selectedRow["opisanie"] = opisanieT.Text;
                    selectedRow["Dostupnost"] = dostupC2.Text;

                    // Возвращение обработчика по умолчанию
                    izmenitB.Click -= izmenitB_Click;
                    izmenitB.Click += izmenitB_Click;
                };
            }
        }


        private void udalitB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void poiskB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
