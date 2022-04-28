﻿using System;
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
    /// Interaction logic for AddGradePage.xaml
    /// </summary>
    public partial class AddGradePage : Page
    {
        private int StudentID { get; set; }
        public AddGradePage(int StudentID)
        {
            this.StudentID = StudentID;
            InitializeComponent();
            using (var context = new Entities())
            {
                var subjects = context.subjects.Select(s => s.subject_name);
                SubjectsComboBox.ItemsSource = subjects.ToList();
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                try
                {
                    var subjectName = SubjectsComboBox.SelectedItem.ToString();
                    Console.WriteLine(subjectName);
                    var subjectId = context.subjects.FirstOrDefault(s => s.subject_name == subjectName).subject_id;
                    grade grade = new grade()
                    {
                        grade_value = int.Parse(GradeTextBox.Text),
                        subject_id = subjectId,
                    };
                    context.grades.Add(grade);
                    context.student_has_grade.Add(new student_has_grade()
                    {
                        grade_id = grade.grade_id,
                        student_id = StudentID
                    });
                    context.SaveChanges();
                    MyPopup.IsOpen = true;
                }
                catch
                {
                    return;
                }
            }
        }
        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            MyPopup.IsOpen = false;
        }
    }
}
