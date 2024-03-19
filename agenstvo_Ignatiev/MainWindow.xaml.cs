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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace agenstvo_Ignatiev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void avtB_click(object sender, RoutedEventArgs e)
        {
            avtorizacia avtorizacia = new avtorizacia();
            avtorizacia.Show();
            this.Close();
        }

        private void regB_click(object sender, RoutedEventArgs e)
        {
            registracia registracia = new registracia();
            registracia.Show();
            this.Close();
        }
    }
}
