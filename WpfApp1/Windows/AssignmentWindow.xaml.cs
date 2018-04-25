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
            RoomSchedule.RoomScheduled = viewModel.CurrentRoom;
            RoomSchedule.CoursesForRoom = viewModel.CoursesForSelectedRoom;
            RoomSchedule.AvailableScheduleSlots = viewModel.AvailableSlots;
        }



        private void AssignCoursesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Course course = AssignCoursesListView.SelectedItem as Course;
            viewModel.CurrentCourse = course;
        }

        private void CurrentCourseChanged()
        {
            if (viewModel.CurrentCourse == null) return;
            CourseDetailControl.SetCourse(viewModel.CurrentCourse);

        }


        private void AvailableRoomsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var room = AvailableRoomsListView.SelectedItem as Room;
            viewModel.CurrentRoom = room;
        }

       
        private void RoomOptionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = RoomOptionsComboBox.SelectedItem as Room;
            viewModel.CurrentCourse.RoomAssignment = room.RoomName;
        }
    }
}
