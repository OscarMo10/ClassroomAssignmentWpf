using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.ViewModel
{
    public class AssignmentViewModel
    {
        public ObservableCollection<Course> CoursesBeingAssigned { get; } = new ObservableCollection<Course>();
        public Course SelectedCourse { get; private set; }
        public ObservableCollection<Room> AvailableRooms { get; } = new ObservableCollection<Room>();

        private RoomSearch roomSearch;


        public AssignmentViewModel(IList<Course> courses)
        {
            foreach (var course in courses)
            {
                CoursesBeingAssigned.Add(course);
            }



            ICourseRepository courseRepository = CourseRepository.GetInstance();
            IRoomRepository roomRepository = RoomRepository.GetInstance();
            roomSearch = new RoomSearch(roomRepository, courseRepository);

            SelectCourse(CoursesBeingAssigned.First());
        }

        public void SelectCourse(Course course)
        {
            while (AvailableRooms.Count != 0)
            {
                AvailableRooms.RemoveAt(0);
            }

            SelectedCourse = course;

            int capacity;
            bool result = int.TryParse(course.RoomCapRequest, out capacity);
            if (!result) capacity = -1;

            List<Room> rooms = roomSearch.
                 AvailableRooms(course.MeetingDays, course.StartTime.Value, course.EndTime.Value, capacity);

            IEnumerable<Room> sortedRoomsByCapacity = rooms.OrderBy(x => x.Capacity);

            foreach (var room in sortedRoomsByCapacity)
            {
                AvailableRooms.Add(room);
            }
        }

    }
}
