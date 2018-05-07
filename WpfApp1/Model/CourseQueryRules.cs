using ClassroomAssignment.Model.Utils;
using ClassroomAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ClassroomAssignment.Model.DataConstants;

namespace ClassroomAssignment.Model
{
    public static class CourseQueryRules
    {
        public static bool QueryNeedsRoom(this ParsedCourse course)
        {
            bool needsRoom = true;
            if (course.InstructionMethod?.Contains(InstructionMethods.OFF_CAMPUS) == true)
            {
                needsRoom = false;
            }
            else if (course.Room?.Contains(RoomOptions.NO_ROOM_NEEDED) == true)
            {
                needsRoom = false;
            }
            else if (course.MeetingPattern?.Contains(MeetingPatternOptions.DOES_NOT_MEET) == true)
            {
                needsRoom = false;
            }

            return needsRoom;
        }

        public static bool QueryHasAmbiguousAssignment(this ParsedCourse course)
        {
            return HasMultipleRoomAssignments(course);
        }

        private static bool HasMultipleRoomAssignments(this ParsedCourse course)
        {
            bool multipleAssignments = false;

            Regex longPKI = new Regex(RoomOptions.PETER_KIEWIT_INSTITUTE_REGEX);
            Regex shortPKI = new Regex(RoomOptions.PKI_REGEX);

            Match roomColMatch = longPKI.Match(course.Room);
            Match commentColMatch = shortPKI.Match(course.Comments);
            Match notesColMatch = shortPKI.Match(course.Notes);

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


        public static List<DayOfWeek> QueryMeetingDays(this ParsedCourse course)
        {
            Regex regex = new Regex(DataConstants.MeetingPatternOptions.TIME_PATTERN);
            Match match = regex.Match(course.MeetingPattern);

            List<DayOfWeek> MeetingDays = new List<DayOfWeek>();
            foreach (Group group in match.Groups)
            {
                DayOfWeek day = DateUtil.AbbreviationToDayOfWeek(group.Value);
                MeetingDays.Add(day);
            }

            return MeetingDays;
        }

        public static TimeSpan? QueryStartTime(this ParsedCourse course)
        {
            Regex regex = new Regex(MeetingPatternOptions.TIME_PATTERN);
            Match match = regex.Match(course.MeetingPattern);
            var startTimeStr = match.Groups[2].Value;

            TimeSpan timeSpan = new TimeSpan();
            if (TimeSpan.TryParse(startTimeStr, out timeSpan))
            {
                return timeSpan;
            }

            return null;            
        }

        public static TimeSpan? QueryEndTime(this ParsedCourse course)
        {
            Regex regex = new Regex(MeetingPatternOptions.TIME_PATTERN);
            Match match = regex.Match(course.MeetingPattern);
            var endTimeStr = match.Groups[3].Value;

            TimeSpan timeSpan = new TimeSpan();
            if (TimeSpan.TryParse(endTimeStr, out timeSpan))
            {
                return timeSpan;
            }

            return null;
        }

        /// <summary>
        /// Returns rooms assignments.
        /// </summary>
        /// <param name="course"></param>
        /// <returns>List of rooms assignments. Multiple rooms if course has ambiguous assignment or
        ///  an empty list if course has none.</returns>
        public static List<string> QueryRoomAssignment(this ParsedCourse course)
        {
            List<string> roomAssignments = new List<string>();

            Regex[] regexs = new Regex[] { new Regex(RoomOptions.PETER_KIEWIT_INSTITUTE_REGEX), new Regex(RoomOptions.PKI_REGEX) };
            string[] courseProperties = new string[] { course.Room, course.Comments, course.Notes };
           
            foreach (var property in courseProperties)
            {
                foreach (var regex in regexs)
                {
                    Match match;
                    if ((match = regex.Match(property)).Success)
                    {
                        var normalizedRoomName = match.Value.Replace("Peter Kiewit Institute", "PKI");
                        roomAssignments.Add(normalizedRoomName);
                    }
                }
            }

            return roomAssignments;
        }


    }
}
