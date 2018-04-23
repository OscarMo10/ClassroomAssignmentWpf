using ClassroomAssignment.Model;
using ClassroomAssignment.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ClassroomAssignment.Windows
{
    /// <summary>
    /// Interaction logic for AssignmentWindow.xaml
    /// </summary>
    public partial class AssignmentWindow : Window
    {
        private AssignmentViewModel viewModel;
        public AssignmentWindow(List<Course> courses)
        {
            InitializeComponent();
            viewModel = new AssignmentViewModel(courses);
            DataContext = viewModel;

            AvailableRoomsListView.ItemsSource = viewModel.AvailableRooms;
            //RoomSchedule.SetCoursesForRoom(viewModel.CoursesForSelectedRoom);

        }

      

        private void AssignCoursesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
             Course course = AssignCoursesListView.SelectedItem as Course;
            viewModel.SelectCourse(course);
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Debug.Write("Hello");
        }

        private void AvailableRoomsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var room = AvailableRoomsListView.SelectedItem as Room;
            viewModel.SelectCurrentRoom(room);
            //RoomSchedule.SetRoom(room);
            //RoomSchedule.SetCoursesForRoom(viewModel.CoursesForSelectedRoom);
        }
    }
}
