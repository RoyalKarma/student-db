﻿
using System;
using System.Data.Entity;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        CollectionViewSource StudentViewSource;
        public List<student> Students { get; set; }

        public HomePage()
        {
            InitializeComponent();
            StudentViewSource = (CollectionViewSource)FindResource("StudentViewSource");
            using (var context = new Entities())
            {
                var query =
                    from s in context.students
                    join shasp in context.student_has_faculty on s.student_id equals shasp.student_id
                    join p in context.faculties on shasp.faculty_id equals p.faculty_id
                    select new
                    {
                        student_id = s.student_id,
                        first_name = s.first_name,
                        last_name = s.last_name,
                        year = s.year,
                        abbrevation = p.abbrevation
                    };
                StudentViewSource.Source = query.ToList();
            };
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStudentPage());
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var context = new Entities())
            {
                var query =
                    from s in context.students
                    join shasp in context.student_has_faculty on s.student_id equals shasp.student_id
                    join p in context.faculties on shasp.faculty_id equals p.faculty_id
                    where s.first_name.Contains(FilterBox.Text) | s.last_name.Contains(FilterBox.Text) 
                    select new
                    {
                        student_id = s.student_id,
                        first_name = s.first_name,
                        last_name = s.last_name,
                        year = s.year,
                        abbrevation = p.abbrevation

                    };
                StudentDataGrid.ItemsSource = query.ToList();
            }
        }

        private void StudentInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentDataGrid.SelectedValue != null)
            {
                NavigationService.Navigate(new StudentInfoPage(ParseStudentID()));
                StudentDataGrid.SelectedValue = null;
                ShowStudentInfoButton.IsEnabled = false;
            }
        }

        private int ParseStudentID()
        {
            string s = StudentDataGrid.SelectedValue.ToString();
            s = s.Substring(1, s.Length - 2).Trim().Split(',')[0].Split('=')[1].Trim();
            return int.Parse(s);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                int id = ParseStudentID();
                var student = (from s in context.students
                               where s.student_id == id
                               select s).SingleOrDefault();
                context.students.Remove(student);
                try { context.SaveChanges(); } catch { return; }
                NavigationService.Refresh();
                DeleteButton.IsEnabled = false;
            }
        }

        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationService != null)
            {
                DeleteButton.IsEnabled = true;
                ShowStudentInfoButton.IsEnabled = true;
            }
        }
    }
}
