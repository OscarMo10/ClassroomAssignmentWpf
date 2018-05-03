using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Model.Utils;
using ClassroomAssignment.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ClassroomAssignment.Model.DataConstants;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;
using ClassroomAssignment.Repo;

namespace ClassroomAssignment.Model
{
    [Serializable]
    public class Course : INotifyPropertyChanged
    {
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

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;


        #region Parsed Properties

        private string _classID;
        public string ClassID
        {
            get => _classID;
            set
            {
                _classID = value;
                OnPropertyChanged();
            }
        }

        private string _SIS_ID;
        public string SIS_ID
        {
            get => _SIS_ID;
            set
            {
                _SIS_ID = value;
                OnPropertyChanged();
            }
        }

        private string _term;
        public string Term
        {
            get => _term;
            set
            {
                _term = value;
                OnPropertyChanged();
            }
        }

        private string _termCode;
        public string TermCode
        {
            get => _termCode;
            set
            {
                _termCode = value;
                OnPropertyChanged();
            }
        }

        private string _departmentCode;
        public string DepartmentCode
        {
            get => _departmentCode;
            set
            {
                _departmentCode = value;
                OnPropertyChanged();
            }
        }

        private string _subjectCode;
        public string SubjectCode
        {
            get => _subjectCode;
            set
            {
                _subjectCode = value;
                OnPropertyChanged();
            }
        }

        private string _catalogNumber;
        public string CatalogNumber
        {
            get => _catalogNumber;
            set
            {
                _catalogNumber = value;
                OnPropertyChanged();
            }
        }

        // Course
        private string _courseName;
        /// <summary>
        /// Property maps to the "Course" column of the deparment spreadsheet.
        /// </summary>
        public string CourseName
        {
            get => _courseName;
            set
            {
                _courseName = value;
                OnPropertyChanged();
            }
        }

        private string _sectionNumber;
        public string SectionNumber
        {
            get => _sectionNumber;
            set
            {
                _sectionNumber = value;
                OnPropertyChanged();
            }
        }

        public string _courseTitle;
        public string CourseTitle
        {
            get => _courseTitle;
            set
            {
                _courseTitle = value;
                OnPropertyChanged();
            }
        }

        private string _sectionType;
        public string SectionType
        {
            get => _sectionType;
            set
            {
                _sectionType = value;
                OnPropertyChanged();
            }
        }

        private string _topic;

        /// <summary>
        /// Property maps to the "Title/Topic" column of the department spreadsheet.
        /// </summary>
        public string Topic
        {
            get => _topic;
            set
            {
                _topic = value;
                OnPropertyChanged();
            }
        }

        private string _meetingPattern;
        public string MeetingPattern
        {
            get => _meetingPattern;
            set
            {
                _meetingPattern = value;
                OnPropertyChanged();
            }
        }

        private string _instructor;
        public string Instructor
        {
            get => _instructor;
            set
            {
                _instructor = value;
                OnPropertyChanged();
            }
        }

        private string _room;
        public string Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged();
            }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        private string _session;
        public string Session
        {
            get => _session;
            set
            {
                _session = value;
                OnPropertyChanged();
            }
        }

        private string _campus;
        public string Campus
        {
            get => _campus;
            set
            {
                _campus = value;
                OnPropertyChanged();
            }
        }

        private string _instructionMethod;
        public string InstructionMethod
        {
            get => _instructionMethod;
            set
            {
                _instructionMethod = value;
                OnPropertyChanged();
            }
        }

        private string _integerPartner;
        public string IntegerPartner
        {
            get => _integerPartner;
            set
            {
                _integerPartner = value;
                OnPropertyChanged();
            }
        }

        private string _schedule;
        public String Schedule
        {
            get => _schedule;
            set
            {
                _schedule = value;
                OnPropertyChanged();
            }
        }

        private string _consent;
        public string Consent
        {
            get => _consent;
            set
            {
                _consent = value;
                OnPropertyChanged();
            }
        }

        private string _creditHrsMin;
        public string CreditHrsMin
        {
            get => _creditHrsMin;
            set
            {
                _creditHrsMin = value;
                OnPropertyChanged();
            }
        }

        private string _creditHrs;
        public string CreditHrs
        {
            get => _creditHrs;
            set
            {
                _creditHrs = value;
                OnPropertyChanged();
            }
        }

        public string _gradeMode;
        public String GradeMode
        {
            get => _gradeMode;
            set
            {
                _gradeMode = value;
                OnPropertyChanged();
            }
        }

        private string _attributes;
        public string Attributes
        {
            get => _attributes;
            set
            {
                _attributes = value;
                OnPropertyChanged();
            }
        }

        private string _roomAttributes;
        public string RoomAttributes
        {
            get => _roomAttributes;
            set
            {
                _roomAttributes = value;
                OnPropertyChanged();
            }
        }

        private string _enrollment;
        public string Enrollment
        {
            get => _enrollment;
            set
            {
                _enrollment = value;
                OnPropertyChanged();
            }
        }

        private string _maximumEnrollment;
        public string MaximumEnrollment
        {
            get => _maximumEnrollment;
            set
            {
                _maximumEnrollment = value;
                OnPropertyChanged();
            }
        }

        private string _priorEnrollment;
        public string PriorEnrollment
        {
            get => _priorEnrollment;
            set
            {
                _priorEnrollment = value;
                OnPropertyChanged();
            }
        }

        private string _projectedEnrollment;
        public string ProjectedEnrollment
        {
            get => _projectedEnrollment;
            set
            {
                _projectedEnrollment = value;
                OnPropertyChanged();
            }
        }

        private string _waitCap;
        public string WaitCap
        {
            get => _waitCap;
            set
            {
                _waitCap = value;
                OnPropertyChanged();
            }
        }

        private string _roomCapRequest;
        public string RoomCapRequest
        {
            get => _roomCapRequest;
            set
            {
                _roomCapRequest = value;
                OnPropertyChanged();
            }
        }

        private string _crossListings;
        public string CrossListings
        {
            get => _crossListings;
            set
            {
                _crossListings = value;
                OnPropertyChanged();
            }
        }

        private string _linkTo;
        public string LinkTo
        {
            get => _linkTo;
            set
            {
                _linkTo = value;
                OnPropertyChanged();
            }
        }

        private string _comments;
        public string Comments
        {
            get => _comments;
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        }

        private string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        #endregion



        #region Derived Properties
        private bool _ambiguousState;
        public bool AmbiguousState {
            get
            {
                bool nowAmbiguous = false;
                if (!NeedsRoom || AlreadyAssignedRoom) nowAmbiguous = false;
                else nowAmbiguous = HasMultipleRoomAssignments();

                if (nowAmbiguous != _ambiguousState)
                {
                    _ambiguousState = nowAmbiguous;
                    OnPropertyChanged();
                }

                return _ambiguousState;
            }

            private set { }
        }

        private bool _needsRoom;
        public bool NeedsRoom
        {
            get => _needsRoom;

            set
            {
                _needsRoom = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Calculated property. Returns true if RoomAssignment has been assigned. Does not guarantee valid assignment.
        /// </summary>
        public bool AlreadyAssignedRoom
        {
            get => RoomAssignment == null ? false : true;
        }

        private Room _roomAssignment;
        public Room RoomAssignment
        {
            get => _roomAssignment;

            set
            {
                if (_roomAssignment != value)
                {
                    _roomAssignment = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(State));
                }

            }
        }
        public List<DayOfWeek> MeetingDays { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        private CourseState _state;
        public CourseState State
        {
            get => _state;

            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        public int ClassID_AsInt
        {
            get => int.Parse(ClassID);
        }

        public string CourseDescription
        {
            get
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

     

        #endregion


        #region Private Methods
        private bool HasMultipleRoomAssignments()
        {
            bool multipleAssignments = false;

            Regex longPKI = new Regex(RoomOptions.PETER_KIEWIT_INSTITUTE_REGEX);
            Regex shortPKI = new Regex(RoomOptions.PKI_REGEX);

            Match roomColMatch = longPKI.Match(Room);
            Match commentColMatch = shortPKI.Match(Comments);
            Match notesColMatch = shortPKI.Match(Notes);

            if (roomColMatch.Success || commentColMatch.Success || notesColMatch.Success)
            {
                if (roomColMatch.Success ^ commentColMatch.Success ^ notesColMatch.Success)
                {
                    multipleAssignments = false; ;
                }
                else
                {
                    multipleAssignments = true;
                }
            }

            return multipleAssignments;

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region Public Methods
        /// <summary>
        /// Convienence method. Sets the MeetingDays, StartTime, EndTime, NeedsRoom, and RoomAssignment, using the other properties of Course.
        /// </summary>
        /// 
        
        public void SetDerivedProperties()
        {
            SetMeetingProperties();
            SetNeedsRoom();
            SetRoomAssignment();
        }


        /// <summary>
        /// Sets the MeetingDays, StartTime, EndTime properties using the state of the Course object.
        /// </summary>
        public void SetMeetingProperties()
        {
            Regex regex = new Regex(DataConstants.MeetingPatternOptions.TIME_PATTERN);
            Match match = regex.Match(MeetingPattern);
            
            if (match.Success)
            {
                var daysCapture = match.Groups[1].Captures;
                MeetingDays = new List<DayOfWeek>();
                foreach (Capture c in daysCapture)
                {
                    DayOfWeek day = DateUtil.AbbreviationToDayOfWeek(c.Value);
                    MeetingDays.Add(day);
                }

                var startTimeStr = match.Groups[2].Value;
                StartTime = TimeUtil.StringToTimeSpan(startTimeStr);
                var endTimeStr = match.Groups[3].Value;
                EndTime = TimeUtil.StringToTimeSpan(endTimeStr);
            }
        }

       

        /// <summary>
        /// Sets the NeedsRoom property using the state of the Course object.
        /// </summary>
        public void SetNeedsRoom()
        {
            if (InstructionMethod?.Equals(InstructionMethods.OFF_CAMPUS) ?? false)
            {
                NeedsRoom = false;
            }
            else if (Room.Equals(RoomOptions.NO_ROOM_NEEDED))
            {
                NeedsRoom = false;
            }
            else if(MeetingPattern.Equals(MeetingPatternOptions.DOES_NOT_MEET))
            {
                NeedsRoom = false;
            }
            else
            {
                NeedsRoom = true;
            }
            
        }

        public void SetRoomAssignment()
        {
            if (AmbiguousState) return;

            Regex longPKI = new Regex(RoomOptions.PETER_KIEWIT_INSTITUTE_REGEX);
            Regex shortPKI = new Regex(RoomOptions.PKI_REGEX);

            Match roomColMatch = longPKI.Match(Room);
            Match commentColMatch = shortPKI.Match(Comments);
            Match notesColMatch = shortPKI.Match(Notes);

            
            if (roomColMatch.Success)
            {
                //IRoomRepository roomRepository = InMemoryRoomRepository.getInstance();
                //var room = roomRepository.getNormalizedRoomName(Room);
                var room = Room.Replace("Peter Kiewit Institute", "PKI");
                RoomAssignment = RoomRepository.GetInstance().GetRoomWithName(room);
            }
            else if (commentColMatch.Success)
            {
                RoomAssignment = RoomRepository.GetInstance().GetRoomWithName(Comments);
            }
            else if (notesColMatch.Success)
            {
                RoomAssignment = RoomRepository.GetInstance().GetRoomWithName(Notes);
            }
        }

        public override bool Equals(object obj)
        {
            var course = obj as Course;
            return course != null &&
                   _SIS_ID == course._SIS_ID &&
                   _term == course._term &&
                   _termCode == course._termCode &&
                   _departmentCode == course._departmentCode &&
                   _subjectCode == course._subjectCode &&
                   _catalogNumber == course._catalogNumber &&
                   _courseName == course._courseName &&
                   _sectionNumber == course._sectionNumber &&
                   _courseTitle == course._courseTitle &&
                   _sectionType == course._sectionType &&
                   _topic == course._topic &&
                   _meetingPattern == course._meetingPattern &&
                   _instructor == course._instructor &&
                   _room == course._room &&
                   _status == course._status &&
                   _session == course._session &&
                   _campus == course._campus &&
                   _instructionMethod == course._instructionMethod &&
                   _integerPartner == course._integerPartner &&
                   _schedule == course._schedule &&
                   _consent == course._consent &&
                   _creditHrsMin == course._creditHrsMin &&
                   _creditHrs == course._creditHrs &&
                   _gradeMode == course._gradeMode &&
                   _attributes == course._attributes &&
                   _roomAttributes == course._roomAttributes &&
                   _enrollment == course._enrollment &&
                   _maximumEnrollment == course._maximumEnrollment &&
                   _priorEnrollment == course._priorEnrollment &&
                   _projectedEnrollment == course._projectedEnrollment &&
                   _waitCap == course._waitCap &&
                   _roomCapRequest == course._roomCapRequest &&
                   _crossListings == course._crossListings &&
                   _linkTo == course._linkTo &&
                   _comments == course._comments &&
                   _notes == course._notes &&
                   EqualityComparer<Room>.Default.Equals(_roomAssignment, course._roomAssignment);
        }

        public override int GetHashCode()
        {
            var hashCode = 683931527;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_SIS_ID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_term);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_termCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_departmentCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_subjectCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_catalogNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_courseName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_sectionNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_courseTitle);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_sectionType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_topic);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_meetingPattern);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_instructor);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_room);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_status);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_session);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_campus);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_instructionMethod);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_integerPartner);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_schedule);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_consent);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_creditHrsMin);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_creditHrs);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_gradeMode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_attributes);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_roomAttributes);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_enrollment);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_maximumEnrollment);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_priorEnrollment);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_projectedEnrollment);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_waitCap);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_roomCapRequest);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_crossListings);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_linkTo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_comments);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_notes);
            hashCode = hashCode * -1521134295 + EqualityComparer<Room>.Default.GetHashCode(_roomAssignment);
            
            return hashCode;
        }

        public static bool operator ==(Course course1, Course course2)
        {
            return EqualityComparer<Course>.Default.Equals(course1, course2);
        }

        public static bool operator !=(Course course1, Course course2)
        {
            return !(course1 == course2);
        }





        #endregion
    }


}
