using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using app.database;
using System.Data.SqlClient;
using System.Text;
using System.Data.Entity.Core.EntityClient;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

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
            //TODO- jak bindovat z více tabulek?
            InitializeComponent();
            string query = "SELECT id_student, first_name, last_name, year, program_name FROM student" +
               " LEFT JOIN student_has_study_program shsp ON shsp.student_id = id_student" +
               " LEFT JOIN study_program sp on sp.program_id = shsp.program_id";
            Students = model.students.SqlQuery(query).ToListAsync().Result;
            StudentGrid.DataContext = Students;
            
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
            try
            {
                string query = "SELECT * FROM student WHERE first_name = @student_name";   
                
                SqlParameter sp = new SqlParameter("student_name", FilterBox.Text);
                
                Students = model.students.SqlQuery(query,sp).ToListAsync().Result;
                if (Output != null)
                {
                    Output.Content = Students.Count;
                    StudentGrid.ItemsSource = Students;
                }
            }
            catch (AggregateException) { }
        }
        private void StudentInfoButton_Click(object sender, RoutedEventArgs e)
        {
        }
       
    }
}
