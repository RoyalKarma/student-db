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
    public partial class AddStudentPage : Page
    {
        public AddStudentPage()
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                var faculties = context.faculties.Select(f => f.faculty_name);
                FacultyComboBox.ItemsSource = faculties.ToList();
                YearComboBox.ItemsSource = Grades.GradeList; // Too lazy to create another class
            }
        }
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var year = short.Parse(YearComboBox.Text);
                student student = new student()
                {
                    first_name = FirstNameTextBox.Text,
                    last_name = LastNameTextBox.Text,
                    year = year,
                };
                var faculty = context.faculties.Where(f => f.faculty_name == FacultyComboBox.Text).Single();
                student_has_faculty faculty_bind = new student_has_faculty()
                {
                    faculty_id = faculty.faculty_id,
                    student_id = student.student_id
                };
                context.students.Add(student);
                context.student_has_faculty.Add(faculty_bind);
                context.SaveChanges();
                PopupTextBlock.Text = "Student successfully added";
                AddStudentPopup.IsOpen = true;
            }
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            AddStudentPopup.IsOpen = false;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
