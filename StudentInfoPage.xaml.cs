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
    /// Interaction logic for Student_info.xaml
    /// </summary>
    public partial class StudentInfoPage : Page
    {
        private UniModel db = new UniModel();
        private int student_id { get; set; } 
        public StudentInfoPage(int id)
        { 
            InitializeComponent();
            student_id = id;
            
            var query = from s in db.students
                        where s.student_id == id
                        join shg in db.student_has_grade on s.student_id equals shg.student_id
                        join g in db.grades on shg.grade_id equals g.grade_id
                        join shs in db.student_has_subject on s.student_id equals shs.student_id
                        join sub in db.subjects on shs.student_id equals sub.subject_id
                        select new
                        {
                            studen_name = s.first_name,
                            subject_name = s.first_name,
                            grade = g.grade1
                        };
            StudentNameLabel.Content = query.ToList().Count;
        }

        private void StudentButton_Click(object sender, RoutedEventArgs e)
        {
            StudentNameLabel.Content = "Hello from other world";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteGradeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ModifyGradeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
