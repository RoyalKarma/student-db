using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using app.model;
using app.database;

namespace app
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private UniversityModel model = new UniversityModel();
        private UniversityDatabaseDataSet dataSet = new UniversityDatabaseDataSet();
        public List<student> Students { get; set; }

        //public int Test { get; set; }

        public Home()
        {
            InitializeComponent();
            DataContext = this;
            Students = model.students.ToListAsync().Result;
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
