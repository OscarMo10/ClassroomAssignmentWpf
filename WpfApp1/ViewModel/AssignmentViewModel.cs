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
        private AssignmentConflictDetector ConflictDetector;
        CourseRepository CourseRepo;

        public event PropertyChangedEventHandler PropertyChanged;

        public AssignmentViewModel(IList<Course> courses)
        {
            foreach (var course in courses)
            {
                CoursesBeingAssigned.Add(course);
            }


            CourseRepo = CourseRepository.GetInstance();
            IRoomRepository roomRepository = RoomRepository.GetInstance();
            RoomSearch = new AvailableRoomSearch(roomRepository, CourseRepo);

            SelectCourse(CoursesBeingAssigned.First());
            AddConflictingCourses();
            SelectCurrentRoom(RoomRepository.GetInstance().AllRooms().First());

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

            List<Room> rooms = RoomSearch.
                 AvailableRooms(course.MeetingDays, course.StartTime.Value, course.EndTime.Value, capacity);

            IEnumerable<Room> sortedRoomsByCapacity = rooms.OrderBy(x => x.Capacity);

            foreach (var room in sortedRoomsByCapacity)
            {
                AvailableRooms.Add(room);
            }

            SelectCurrentRoom(AvailableRooms.FirstOrDefault());

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCourse)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableRooms)));
        }

        public void SelectCurrentRoom(Room room)
        {
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
