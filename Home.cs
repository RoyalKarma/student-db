using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using app.model;
using System.Linq;

namespace app
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private UniversityModel db = new UniversityModel();
        public List<student> Students { get; set; }

        public Home()
        {          
            InitializeComponent();

            var query =
               from s in db.students
               join shasp in db.student_has_study_program on s.id_student equals shasp.student_id
               join p in db.study_program on shasp.program_id equals p.program_id
               select new { 
                   first_name = s.first_name, 
                   last_name = s.last_name,
                   year = s.year,
                   program_name = p.program_name
               };

            StudentGrid.DataContext = query.ToList();
            
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
                
                Students = db.students.SqlQuery(query,sp).ToListAsync().Result;
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
