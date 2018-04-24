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
        public IEnumerable<Course> CoursesForSelectedRoom = new List<Course>();
        public IEnumerable<ScheduleSlot> AvailableSlots = new List<ScheduleSlot>();

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

            CourseSelectedForAssignment(CoursesBeingAssigned.First());
            AddConflictingCourses();
        }

        public void CourseSelectedForAssignment(Course course)
        {
            SelectedCourse = course;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCourse)));
            UpdateAvailableRoomsForSelectedCourse(course);
        }

        private void UpdateAvailableRoomsForSelectedCourse(Course course)
        {
            RemoveStaleAvailableRooms();
            AddAvailableRooms(course);
            SetCurrentRoom(AvailableRooms.FirstOrDefault());

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

       
        private void RemoveStaleAvailableRooms()
        {
            while (AvailableRooms.Count != 0)
            {
                AvailableRooms.RemoveAt(0);
            }
        }

        public void SetCurrentRoom(Room room)
        {
            if (room == null) return;

            CoursesForSelectedRoom = from course in CourseRepo.Courses
                                    where course.RoomAssignment == room.RoomName
                                    select course;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoursesForSelectedRoom)));


            var searchParameters = new SearchParameters();
            searchParameters.MeetingDays = SelectedCourse.MeetingDays;
            searchParameters.StartTime = SelectedCourse.StartTime.Value;
            searchParameters.EndTime = SelectedCourse.EndTime.Value;
            searchParameters.Capacity = int.Parse(SelectedCourse.RoomCapRequest);
            searchParameters.Duration = SelectedCourse.EndTime.Value - SelectedCourse.StartTime.Value;

            List<ScheduleSlot> slots = RoomSearch.ScheduleSlotsAvailable(searchParameters);
            AvailableSlots = slots.FindAll(x => x.RoomAvailable == room);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableSlots)));
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
