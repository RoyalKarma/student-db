using app.model;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private UniModel db = new UniModel();
        public List<student> Students { get; set; }

        public HomePage()
        {
            InitializeComponent();

            var query =
               from s in db.students
               join shasp in db.student_has_study_program on s.student_id equals shasp.student_id
               join p in db.study_program on shasp.program_id equals p.program_id
               select new
               {
                   id = s.student_id,
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
            var query =
               from s in db.students
               join shasp in db.student_has_study_program on s.student_id equals shasp.student_id
               join p in db.study_program on shasp.program_id equals p.program_id
               where s.first_name.Contains(FilterBox.Text) | p.program_name.Contains(FilterBox.Text)//| s.last_name.Contains(FilterBox.Text) 

               select new
               {
                   id = s.student_id,
                   first_name = s.first_name,
                   last_name = s.last_name,
                   year = s.year,
                   program_name = p.program_name
               };





            StudentGrid.ItemsSource = query.ToList();
        }
        private void StudentInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if(StudentGrid.SelectedValue != null)
            {
                NavigationService.Navigate(new StudentInfoPage(parseID()));
                StudentGrid.SelectedValue = null;
            }
            
            
        }
        
        private int parseID() {
            string s = StudentGrid.SelectedValue.ToString();
            s = s.Substring(1, s.Length - 2).Trim().Split(',')[0].Split('=')[1].Trim();
            return int.Parse(s); 
        }
    }
}
