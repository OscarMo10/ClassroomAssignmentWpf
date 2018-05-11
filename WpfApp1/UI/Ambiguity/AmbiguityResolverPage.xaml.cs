using ClassroomAssignment.Model;
using ClassroomAssignment.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ClassroomAssignment.UI.Ambiguity
{
    /// <summary>
    /// Interaction logic for AmbiguityResolverPage.xaml
    /// </summary>
    public partial class AmbiguityResolverPage : Page
    {
        private List<Course> _ambiguousCourses;


        public AmbiguityResolverPage()
        {
            InitializeComponent();

            var allCourses = CourseRepository.GetInstance().Courses;

            _ambiguousCourses = allCourses.ToList().FindAll(m => m.HasAmbiguousAssignment);

            CoursesDataGrid.ItemsSource = _ambiguousCourses;

            //_ambiguousCourses.ForEach(RegisterNotificationListener);
        }


        //private void RegisterNotificationListener(Course course)
        //{
        //    course.PropertyChanged += new PropertyChangedEventHandler(OnCoursesStateChanged);
        //}

        //public void OnCoursesStateChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (!AmbiguousCoursesExists())
        //    {
        //        ContinueButton.IsEnabled = true;
        //    }
        //    else
        //    {
        //        ContinueButton.IsEnabled = false;
        //    }
        //}

        //private bool AmbiguousCoursesExists()
        //{
        //    return _ambiguousCourses.FindAll(m => m.HasAmbiguousAssignment).Count > 0;
        //}


        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var course in _ambiguousCourses)
            {
                course.HasAmbiguousAssignment = false;
            }

            NavigationService.Navigate(new Uri(@"UI/Main/MainPage.xaml", UriKind.Relative));
        }
    }

}
