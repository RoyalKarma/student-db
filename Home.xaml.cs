using System;
using System.Windows;
using System.Windows.Controls;

namespace app
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private String simpleData = "RNG BUTTON";
        private Customer[] customers = 
        {
            new Customer("Dodo1", "Marco1", "Dajaka adresa1"),
            new Customer("Dodo2", "Marco2", "Dajaka adresa2"),
            new Customer("Dodo3", "Marco3", "Dajaka adresa3"),
            new Customer("Dodo4", "Marco4", "Dajaka adresa4") 
        };

        public Customer[] Customers 
        { 
            get { return customers; } 
            set { customers = value; }
        }

        public String SimpleData
        {
            get { return simpleData; }
            set { simpleData = value; }
        }

        public Home()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StudentInfoButton_Click(object sender, RoutedEventArgs e)
        { 
        }
    }
}
