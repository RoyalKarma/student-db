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
    public partial class StudentInfoPage : Page
    {
        CollectionViewSource GradeViewSource;
        private int StudentID { get; set; }
        public StudentInfoPage(int StudentID)
        {
            this.StudentID = StudentID;
            InitializeComponent();
            using (var context = new Entities())
            {
                StudentNameLabel.Content = context.students.Where(
                    s => s.student_id.Equals(StudentID)).Select(
                    s => s.first_name + " " + s.last_name).Single().ToString();
                FilterGradeComboBox.SelectedIndex = -1;
                FilterGradeComboBox.ItemsSource = Grades.GradeList;
            }
            GradeViewSource = (CollectionViewSource)FindResource("GradeViewSource");
            SetGrades();
            SetState();
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddGradePage(StudentID));
        }

        private void EditGradeButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(GradeDataGrid.SelectedItem);
            NavigationService.Navigate(new EditGradePage(GradeDataGrid.SelectedItem.ToString()));
            SetGrades();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DeleteGradeButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                int id = ParseGradeID();
                var query = context.grades.Where(g => g.grade_id == id).SingleOrDefault();
                context.grades.Remove(query);
                try { context.SaveChanges(); } catch { return; }
                SetGrades();
                SetState();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationService != null)
            {
                DeleteGradeButton.IsEnabled = true;
                EditGradeButton.IsEnabled = true;
            }
        }

        private int ParseGradeID()
        {
            string s = GradeDataGrid.SelectedValue.ToString();
            s = s.Substring(1, s.Length - 2).Trim().Split(',')[0].Split('=')[1].Trim();
            return int.Parse(s);
        }

        private void SetGrades()
        {
            using (var context = new Entities())
            {
                var query =
                    from g in context.grades
                    join shasg in context.student_has_grade on g.grade_id equals shasg.grade_id
                    join s in context.students on shasg.student_id equals s.student_id
                    join sub in context.subjects on g.subject_id equals sub.subject_id
                    where s.student_id == StudentID
                    select new
                    {
                        grade_id = g.grade_id,
                        grade_value = g.grade_value,
                        subject_name = sub.subject_name,
                    };
                var res = query.Average(g => g.grade_value);
                if (res == null) AverageGradeBox.Content = "0.00";
                else AverageGradeBox.Content = Math.Round((double)res, 2);
                GradeViewSource.Source = query.ToList();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) //clunky af
        {
            SetGrades();
            SetState();
        }

        private void SetState()
        {
            FilterGradeComboBox.SelectedIndex = -1;
            GradeDataGrid.SelectedIndex = -1;
            FilterSubjectTextBox.Text = "";
            DeleteGradeButton.IsEnabled = false;
            EditGradeButton.IsEnabled = false;
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetState();
            Refresh();
        }

        private void FilterGradeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh() {
            using (var context = new Entities())
            {
                var query =
                    from g in context.grades
                    join shasg in context.student_has_grade on g.grade_id equals shasg.grade_id
                    join s in context.students on shasg.student_id equals s.student_id
                    join sub in context.subjects on g.subject_id equals sub.subject_id
                    where s.student_id == StudentID & sub.subject_name.Contains(FilterSubjectTextBox.Text)
                    select new
                    {
                        grade_id = g.grade_id,
                        grade_value = g.grade_value,
                        subject_name = sub.subject_name,
                    };
                if (FilterGradeComboBox.Text != "")
                {
                    var grade = short.Parse(FilterGradeComboBox.Text);
                    var grades = query.Where(g => g.grade_value == grade).ToList();
                    GradeDataGrid.ItemsSource = grades;
                }
                else {
                    GradeDataGrid.ItemsSource = query.ToList();
                }
                var res = query.Average(g => g.grade_value);
                if (res == null) AverageGradeBox.Content = "0.00";
                else AverageGradeBox.Content = Math.Round((double)res, 2);
            }
        }
    }
}
