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
    /// Логика взаимодействия для glavnaya.xaml
    /// </summary>
    public partial class glavnaya : Window
    {
        public glavnaya()
        {
            Loaded += glavnaya_Loaded;
            InitializeComponent();
        }
        private void glavnaya_Loaded(object sender, RoutedEventArgs e)
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
                    }
                    reader.Close();

                    string familiiQuery = "SELECT DISTINCT kolichestvo_komnat FROM [Agenstvo_nedvijimost]";
                    SqlCommand familiiCommand = new SqlCommand(familiiQuery, connection);
                    SqlDataReader reader1 = familiiCommand.ExecuteReader();
                    while (reader1.Read())
                    {
                        komnatC.Items.Add(reader1["kolichestvo_komnat"].ToString());
                    }
                    reader1.Close();

                    string DostupnostQuery = "SELECT DISTINCT Dostupnost FROM [Agenstvo_nedvijimost]";
                    SqlCommand DostupnostCommand = new SqlCommand(DostupnostQuery, connection);
                    SqlDataReader reader2 = DostupnostCommand.ExecuteReader();
                    while (reader2.Read())
                    {
                        dostupC.Items.Add(reader2["Dostupnost"].ToString());
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

        private void poiskB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
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
    }
}
