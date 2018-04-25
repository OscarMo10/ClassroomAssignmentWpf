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
using static ClassroomAssignment.Model.Course;
using static ClassroomAssignment.Extension.CourseExtensions;

namespace ClassroomAssignment.ViewModel
{
    public class AssignmentViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Course> CoursesBeingAssigned { get; } = new ObservableCollection<Course>();
        public List<Room> AllRooms { get; } = RoomRepository.GetInstance().Rooms;

        private Course _currentCourse;
        public Course CurrentCourse
        {
            get => _currentCourse;
            set
            {
                _currentCourse = value;
                _currentCourse.PropertyChanged += _currentCourse_PropertyChanged;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCourse)));
                UpdateAvailableRoomsForSelectedCourse(CurrentCourse);
            }
        }

        private void _currentCourse_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //UpdateCoursesForCurrentRoom(CurrentRoom);
            //UpdateAvailableRoomsForSelectedCourse(CurrentCourse);
            //UpdateAvailableSlotForCurrentRoom(CurrentRoom);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCourse)));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableSlots)));

        }

        private Room _currentRoom;
        public Room CurrentRoom
        {
            get => _currentRoom;
            set
            {
                Room room = value;
                if (room == null) return;
                _currentRoom = value;
                UpdateCoursesForCurrentRoom();
                UpdateAvailableSlotForCurrentRoom();


                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableSlots)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentRoom)));

            }
        }

        public void UpdateAvailableSlotForCurrentRoom()
        {
            var searchParameters = CurrentCourse.GetSearchParameters();

            List<ScheduleSlot> slots = RoomSearch.ScheduleSlotsAvailable(searchParameters);
            AvailableSlots.Clear();
            foreach (var slot in slots.FindAll(x => x.RoomAvailable == CurrentRoom))
            {
                AvailableSlots.Add(slot);
            }
        }

        public void UpdateCoursesForCurrentRoom()
        {
            if (CurrentRoom == null) return;
            var room = CurrentRoom;
            var courses = from course in CourseRepo.Courses
                          where course.RoomAssignment == room.RoomName
                          select course;

            CoursesForSelectedRoom.Clear();
            foreach (Course course in courses)
            {
                CoursesForSelectedRoom.Add(course);
            }
        }

        public ObservableCollection<Room> AvailableRooms { get; } = new ObservableCollection<Room>();
        public ObservableCollection<Course> CoursesForSelectedRoom = new ObservableCollection<Course>();
        public ObservableCollection<ScheduleSlot> AvailableSlots = new ObservableCollection<ScheduleSlot>();

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

            CurrentCourse = CoursesBeingAssigned.First();
            AddConflictingCourses();
        }



        private void UpdateAvailableRoomsForSelectedCourse(Course course)
        {
            RemoveStaleAvailableRooms();
            AddAvailableRooms(course);
            CurrentRoom = AvailableRooms.FirstOrDefault();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableRooms)));
        }

        private void AddAvailableRooms(Course course)
        {
            int capacity = int.MaxValue;
            bool result = int.TryParse(course.RoomCapRequest, out capacity);
            IEnumerable<Room> rooms = RoomSearch.
                 AvailableRooms(course.MeetingDays, course.StartTime.Value, course.EndTime.Value, capacity);

            foreach (var room in rooms.OrderBy(x => x.Capacity))
            {
                AvailableRooms.Add(room);
            }
        }

       
        public void RemoveStaleAvailableRooms()
        {
            while (AvailableRooms.Count != 0)
            {
                AvailableRooms.RemoveAt(0);
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
