using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
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
using System.Windows.Shapes;

namespace ClassroomAssignment
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AmbiguityResolverWindow : Window, INotifyPropertyChanged
    {
        private List<Course> _ambiguousCourses;

        public event PropertyChangedEventHandler PropertyChanged;

        public AmbiguityResolverWindow()
        {
            InitializeComponent();

            var allCourses = CourseRepository.GetInstance().Courses;

            _ambiguousCourses = allCourses.ToList().FindAll(m => m.AmbiguousState);
            
            CoursesDataGrid.ItemsSource = _ambiguousCourses;

            this.Loaded += new RoutedEventHandler(Window_OnLoaded);
            this.Closed += new EventHandler(Window_OnClosed);
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


        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CourseDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
