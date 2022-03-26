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

namespace app
{
    /// <summary>
    /// Interaction logic for Student_info.xaml
    /// </summary>
    public partial class Student_info : Page
    {

        public Customer customers = new Customer("Dodo", "Marco", "Dajaka adresa");
        public string text = "111";

        public Customer GetCustomer { get; set; }

        public Student_info()
        {
            InitializeComponent();
        }

        private void StudentButton_Click(object sender, RoutedEventArgs e)
        {
            StudentLabel.Content = "Hello from other world";
        }
    }
}
