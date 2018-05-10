using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassroomAssignment.Model.CourseQueryRules;

namespace ClassroomAssignment.Model
{
    [Serializable]
    public class Course : ParsedCourse, INotifyPropertyChanged
    {
        public Course()
        {
            _crossListedCourses = new ObservableCollection<Course>();
            _crossListedCourses.CollectionChanged += _crossListedCourses_CollectionChanged;
        }

        private void _crossListedCourses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CrossListedCourses)));
        }

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        public enum CourseState
        {
            [Description("Conflicting")]
            Conflicting,
            [Description("Ambigious Assignment Courses")]
            Ambiguous,
            [Description("Unassigned Courses")]
            Unassigned,
            [Description("Assigned Courses")]
            Assigned,
            [Description("No Room Required")]
            NoRoomRequired
        };

        #region Query Properties

        public bool HasRoomAssignment => RoomAssignment != null;
        public bool HasAmbiguousAssignment => RoomAssignment != null && this.QueryHasAmbiguousAssignment();

        #endregion

        public Room RoomAssignment { get; set; }

        public bool NeedsRoom { get; set; }
        public List<DayOfWeek> MeetingDays { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public CourseState State { get; set; }

        public void AddCrossListedCourse(Course course) => _crossListedCourses.Add(course);

        private ObservableCollection<Course> _crossListedCourses;
        public List<Course> CrossListedCourses
        {
            get => _crossListedCourses.ToList();
            private set { }
        }

        public int ClassID_AsInt => int.Parse(ClassID);

        public void SetAllDerivedProperties()
        {
            var roomName = this.QueryRoomAssignment().FirstOrDefault();
            RoomAssignment = RoomRepository.GetInstance().GetRoomWithName(roomName);
            NeedsRoom = this.QueryNeedsRoom();
            MeetingDays = this.QueryMeetingDays();
            StartTime = this.QueryStartTime();
            EndTime = this.QueryEndTime();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder()
                    .Append(CourseName)
                    .AppendLine()
                    .AppendFormat("Sect. {0}", SectionNumber)
                    .AppendLine();
            var instructors = Instructor.Split(new char[] { ';' });

            foreach (var instructor in instructors)
            {
                stringBuilder.Append(instructor);
                stringBuilder.AppendLine();
            }

            stringBuilder.Append(MeetingPattern);

            return stringBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            var course = obj as Course;
            return course != null &&
                   base.Equals(obj) &&
                   HasRoomAssignment == course.HasRoomAssignment &&
                   EqualityComparer<Room>.Default.Equals(RoomAssignment, course.RoomAssignment) &&
                   NeedsRoom == course.NeedsRoom &&
                   EqualityComparer<List<DayOfWeek>>.Default.Equals(MeetingDays, course.MeetingDays) &&
                   EqualityComparer<TimeSpan?>.Default.Equals(StartTime, course.StartTime) &&
                   EqualityComparer<TimeSpan?>.Default.Equals(EndTime, course.EndTime) &&
                   State == course.State;
        }

       

        public static bool operator ==(Course course1, Course course2)
        {
            return EqualityComparer<Course>.Default.Equals(course1, course2);
        }

        public static bool operator !=(Course course1, Course course2)
        {
            return !(course1 == course2);
        }
    }
}
