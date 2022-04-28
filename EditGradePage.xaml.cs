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
    /// Interaction logic for EditGradePage.xaml
    /// </summary>
    public partial class EditGradePage : Page
    {
        CollectionViewSource GradeViewSource;
        private int GradeID { get; set; }
        public EditGradePage(int GradeID)
        {
            this.GradeID = GradeID;
            InitializeComponent();
            grade_idTextBox.Text = GradeID.ToString();
            using (var context = new Entities())
            {
                var subjects = context.subjects.Select(s => s.subject_name);
                subjectComboBox.ItemsSource = subjects.ToList();
            }


        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var query = context.grades.First(x => x.grade_id == GradeID);
               
                query.grade_value = int.Parse(grade_valueTextBox.Text);
                var subject = context.subjects.Where(s => s.subject_name == subjectComboBox.Text).Single();
                query.subject_id = subject.subject_id;
                context.SaveChanges();
                



            }

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
