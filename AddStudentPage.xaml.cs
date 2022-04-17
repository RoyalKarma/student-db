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
    /// Interaction logic for AddStudentPage.xaml
    /// </summary>
    public partial class AddStudentPage : Page
    {
        UniDBEntities1 context = new UniDBEntities1();
        public AddStudentPage()
        {
            InitializeComponent();
           
        }
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new UniDBEntities1())
            {
                student newStudent = new student()
                {
                    first_name =  first_nameTextBox.Text,
                    last_name = last_nameTextBox.Text,
                    year = short.Parse(yearTextBox.Text),

                };
                
                student_has_faculty faculty_bind = new student_has_faculty() 
                {
                    faculty_id =  int.Parse(facultyIDBox.SelectedIndex.ToString()),
                    student_id = newStudent.student_id
                };
                context.students.Add(newStudent);
                context.student_has_faculty.Add(faculty_bind);
                Console.WriteLine( faculty_bind.faculty_id );
                Console.WriteLine(context.SaveChanges());
            }
        }
     
    }
}
