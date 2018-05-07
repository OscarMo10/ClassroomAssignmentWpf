using System;
using System.Collections.Generic;
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
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        public enum CourseState
        {
            [Description("Ambigious Assignment Courses")]
            Ambiguous,
            [Description("Unassigned Courses")]
            Unassigned,
            [Description("Assigned Courses")]
            Assigned,
            [Description("No Room Required")]
            NoRoomRequired,
            [Description("Conflicting")]
            Conflicting
        };

        #region Query Properties

        public bool HasRoomAssignment => RoomAssignment != null;
        public bool HasAmbiguousAssignment => string.IsNullOrEmpty(RoomAssignment) && this.QueryHasAmbiguousAssignment();

        #endregion

        public string RoomAssignment { get; set; }
        public bool NeedsRoom { get; set; }
        public List<DayOfWeek> MeetingDays { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public CourseState State { get; set; }

        public int ClassID_AsInt => int.Parse(ClassID);

        public void SetAllDerivedProperties()
        {
            RoomAssignment = this.QueryRoomAssignment().FirstOrDefault();
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
    }
}
