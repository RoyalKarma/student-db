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
    /// Interaction logic for EditStudentPage.xaml
    /// </summary>
    public partial class EditStudentPage : Page
    {
        private int StudentID { get; set; }
        public EditStudentPage(int StudentID)
        {
            this.StudentID = StudentID;
            InitializeComponent();
            using (var context = new Entities())
            {
                var query = (
                    from s in context.students
                    join shasp in context.student_has_faculty on s.student_id equals shasp.student_id
                    join f in context.faculties on shasp.faculty_id equals f.faculty_id
                    where s.student_id == StudentID
                    select new
                    {
                        student_id = s.student_id,
                        first_name = s.first_name,
                        last_name = s.last_name,
                        year = s.year,
                        faculty_name = f.faculty_name

                    }).Single();
                StudentFacultyComboBox.ItemsSource = context.faculties.Select(f => f.faculty_name).ToList();
                StudentYearComboBox.ItemsSource = new short[] { 1, 2, 3, 4, 5 };
                StudentIdLabel.Content = StudentID;
                StudentNameTextBox.Text = query.first_name;
                StudentSurnameTextBox.Text = query.last_name;
                StudentYearComboBox.SelectedItem = query.year;
                StudentFacultyComboBox.SelectedItem = query.faculty_name;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var student = context.students.Where(s => s.student_id.Equals(StudentID)).Single();
                var faculty = context.faculties.Where(f => f.faculty_name.Equals(StudentFacultyComboBox.SelectedItem.ToString())).Single();
                var studentHasFaculty = context.student_has_faculty.Where(s => s.student_id.Equals(StudentID)).Single();
                context.student_has_faculty.Remove(studentHasFaculty);
                var newStudentHasFaculty = new student_has_faculty();
                newStudentHasFaculty.faculty_id = faculty.faculty_id;
                newStudentHasFaculty.student_id = student.student_id;
                context.student_has_faculty.Add(newStudentHasFaculty);
                student.first_name = StudentNameTextBox.Text;
                student.last_name = StudentSurnameTextBox.Text;
                student.year = short.Parse(StudentYearComboBox.Text);
                context.SaveChanges();
            }
            StudentEditPopup.IsOpen = true;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            StudentEditPopup.IsOpen = false;
        }
    }
}
