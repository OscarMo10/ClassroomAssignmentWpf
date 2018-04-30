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

namespace ClassroomAssignment.Windows
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

            _ambiguousCourses = allCourses.ToList().FindAll(m => m.AmbiguousState);

            CoursesDataGrid.ItemsSource = _ambiguousCourses;

            this.Loaded += new RoutedEventHandler(Window_OnLoaded);
            this.Unloaded += new RoutedEventHandler(Window_OnClosed);
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            _ambiguousCourses.ForEach(RegisterNotificationListener);

        }

        private void Window_OnClosed(object sender, EventArgs e)
        {
            _ambiguousCourses.ForEach(UnsubscribeListener);
        }

        private void RegisterNotificationListener(Course course)
        {
            course.PropertyChanged += new PropertyChangedEventHandler(OnCoursesStateChanged);
        }

        private void UnsubscribeListener(Course course)
        {
            course.PropertyChanged -= OnCoursesStateChanged;
        }

        public void OnCoursesStateChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!AmbiguousCoursesExists())
            {
                ContinueButton.IsEnabled = true;
            }
            else
            {
                ContinueButton.IsEnabled = false;
            }
        }

        private bool AmbiguousCoursesExists()
        {
            return _ambiguousCourses.FindAll(m => m.AmbiguousState).Count > 0;
        }


        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(@"Windows/MainPage.xaml", UriKind.Relative));
        }
    }

}
