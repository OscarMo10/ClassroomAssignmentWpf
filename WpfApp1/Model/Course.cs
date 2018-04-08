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

namespace ClassroomAssignment.Model
{
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
            NoRoomRequired
        };

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
                if (!NeedsRoom || AlreadyAssignedRoom) nowAmbiguous =  false;
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
            get => string.IsNullOrEmpty(RoomAssignment) ? false : true;
        }

        private string _roomAssignment;
        public string RoomAssignment
        {
            get => _roomAssignment;

            set
            {
                if (_roomAssignment != value)
                {
                    _roomAssignment = value;
                    OnPropertyChanged();
                }
               
            }
        }
        public List<DayOfWeek> MeetingDays { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public CourseState Type
        {
            get
            {
                if (AmbiguousState) return CourseState.Ambiguous;
                if (AlreadyAssignedRoom) return CourseState.Assigned;
                else if (NeedsRoom) return CourseState.Unassigned;
                else return CourseState.NoRoomRequired;
            }
        }

        public int ClassID_AsInt
        {
            get => int.Parse(ClassID);
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
                RoomAssignment = room;
            }
            else if (commentColMatch.Success)
            {
                RoomAssignment = Comments;
            }
            else if (notesColMatch.Success)
            {
                RoomAssignment = Notes;
            }
        }

        #endregion
    }


}
