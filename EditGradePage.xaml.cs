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
        public EditGradePage(string gradeString)
        { 
            InitializeComponent();
            using (var context = new Entities())
            {
                var subjects = context.subjects.Select(s => s.subject_name);
                subjectComboBox.ItemsSource = subjects.ToList();
            }
            ParseDefaultsFromStr(gradeString);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                try
                {
                    var gradeId = int.Parse(grade_idLabel.Content.ToString());
                    var query = context.grades.First(x => x.grade_id == gradeId);
                    var gradeValue = int.Parse(grade_valueTextBox.Text);
                    if (gradeValue > 5 || gradeValue < 1) {
                        throw new FormatException();
                    }
                    query.grade_value = gradeValue;
                    var subject = context.subjects.Where(s => s.subject_name == subjectComboBox.Text).Single();
                    query.subject_id = subject.subject_id;
                    context.SaveChanges();
                    PopupTextBlock.Text = "Grade edited successfully";
                    GradeEditPopup.IsOpen = true;
                }
                catch (FormatException) {
                    PopupTextBlock.Text = "Please input valid grade (1-5)";
                    GradeEditPopup.IsOpen = true;
                    return;
                }
            }

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ParseDefaultsFromStr(string str)
        {
            var s = str.Substring(1, str.Length - 2).Trim().Split(',');
            grade_idLabel.Content = s[0].Split('=')[1].Trim();
            grade_valueTextBox.Text = s[1].Split('=')[1].Trim();
            subjectComboBox.SelectedValue = s[2].Split('=')[1].Trim();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            GradeEditPopup.IsOpen = false;
        }
    }
}
