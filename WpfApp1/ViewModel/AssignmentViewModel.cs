using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Operations;
using ClassroomAssignment.Repo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.ViewModel
{
    public class AssignmentViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Course> CoursesBeingAssigned { get; } = new ObservableCollection<Course>();
        public Course SelectedCourse { get; private set; }
        public ObservableCollection<Room> AvailableRooms { get; } = new ObservableCollection<Room>();
        public ObservableCollection<Course> CoursesForSelectedRoom = new ObservableCollection<Course>();

        private AvailableRoomSearch RoomSearch;
        private CourseRepository CourseRepo = CourseRepository.GetInstance();

        public event PropertyChangedEventHandler PropertyChanged;

        public AssignmentViewModel(IList<Course> courses)
        {
            foreach (var course in courses)
            {
                CoursesBeingAssigned.Add(course);
            }


            IRoomRepository roomRepository = RoomRepository.GetInstance();
            RoomSearch = new AvailableRoomSearch(roomRepository, CourseRepo);

            SelectCourse(CoursesBeingAssigned.First());
            AddConflictingCourses();
        }

        public void SelectCourse(Course course)
        {
            SelectedCourse = course;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCourse)));
            UpdateAvailableRoomsForSelectedCourse(course);
        }

        private void UpdateAvailableRoomsForSelectedCourse(Course course)
        {
            RemoveStaleAvailableRooms();
            AddAvailableRooms(course);
            SelectCurrentRoom(AvailableRooms.FirstOrDefault());

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableRooms)));
        }

        private void AddAvailableRooms(Course course)
        {
            int capacity = int.MaxValue;
            bool result = int.TryParse(course.RoomCapRequest, out capacity);
            List<Room> rooms = RoomSearch.
                 AvailableRooms(course.MeetingDays, course.StartTime.Value, course.EndTime.Value, capacity);

            foreach (var room in rooms.OrderBy(x => x.Capacity))
            {
                AvailableRooms.Add(room);
            }
        }

       
        private void RemoveStaleAvailableRooms()
        {
            while (AvailableRooms.Count != 0)
            {
                AvailableRooms.RemoveAt(0);
            }
        }

        public void SelectCurrentRoom(Room room)
        {
            if (room == null) return;

            var allCoursesForRoom = from course in CourseRepo.Courses
                                    where course.RoomAssignment == room.RoomName
                                    select course;

            while (CoursesForSelectedRoom.FirstOrDefault() != null)
            {
                CoursesForSelectedRoom.RemoveAt(0);
            }

            foreach (var course in allCoursesForRoom)
            {
                CoursesForSelectedRoom.Add(course);
            }
        }

        public void AddConflictingCourses()
        {
            List<Conflict> conflicts = CourseRepo.GetConflictsInvolvingCourses(CoursesBeingAssigned.ToList());

            foreach (var conflict in conflicts)
            {
                foreach (var courseInConflict in conflict.ConflictingCourses)
                {
                    if (!CoursesBeingAssigned.Contains(courseInConflict))
                    {
                        CoursesBeingAssigned.Add(courseInConflict);
                    }
                }
            }
        }

    }
}
