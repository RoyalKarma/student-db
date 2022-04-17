
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
        UniDBEntities1 db = new UniDBEntities1();
        CollectionViewSource studentViewSource;

        public List<student> Students { get; set; }

        public HomePage()
        {

            InitializeComponent();
            studentViewSource = ((CollectionViewSource)(FindResource("studentViewSource")));
            var query =
                from s in db.students
                join shasp in db.student_has_faculty on s.student_id equals shasp.student_id
                join p in db.faculties on shasp.faculty_id equals p.faculty_id
                select new
                {
                    student_id = s.student_id,
                    first_name = s.first_name,
                    last_name = s.last_name,
                    year = s.year,
                    abbrevation = p.abbrevation
                   
                };
            DataContext = this;
            db.students.Load();
            studentViewSource.Source = query.ToList();




        }



        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {



           NavigationService.Navigate(new AddStudentPage());
            /*using (var context = new UniDBEntities1()) { 
            student newStudent = new student()
            {
                first_name = "Jmeno",
                last_name = "Prijmeni",
                year = 1,
                
            };
            student_has_faculty faculty_bind = new student_has_faculty() { faculty_id = 1, student_id = newStudent.student_id };
                context.students.Add(newStudent);
                context.student_has_faculty.Add(faculty_bind);
                Console.WriteLine(context.SaveChanges()); 
            }*/




        }



        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query =
                from s in db.students
                join shasp in db.student_has_faculty on s.student_id equals shasp.student_id
               join p in db.faculties on shasp.faculty_id equals p.faculty_id          
                where s.first_name.Contains(FilterBox.Text) | p.faculty_name.Contains(FilterBox.Text)//| s.last_name.Contains(FilterBox.Text) 
                select new
                {
                    student_id = s.student_id,
                    first_name = s.first_name,
                    last_name = s.last_name,
                    year = s.year,
                    abbrevation = p.abbrevation

                };





            studentDataGrid.ItemsSource = query.ToList();
        }
        private void StudentInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if(studentDataGrid.SelectedValue != null)
            {
                //NavigationService.Navigate(new StudentInfoPage(parseID()));
                studentDataGrid.SelectedValue = null;
            }
            
            
        }
        
        private int parseID() { 
            string s = studentDataGrid.SelectedValue.ToString();
            s = s.Substring(1, s.Length - 2).Trim().Split(',')[0].Split('=')[1].Trim();
            return int.Parse(s); 
        }

        private void studentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            int id = parseID();
            var student = (from s in db.students
                           where s.student_id == id
                           select s).SingleOrDefault();
            db.students.Remove(student);
            db.SaveChanges();
            this.NavigationService.Refresh();

        }
    }
}
