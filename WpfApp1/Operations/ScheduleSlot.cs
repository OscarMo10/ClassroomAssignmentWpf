using ClassroomAssignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Operations
{
    /// <summary>
    /// Schedule Slots, create methods for RoomAvailable,
    /// meetingDays,StartTime and EndTime.
    /// </summary>
    public struct ScheduleSlot
    {
        public Room RoomAvailable;
        public IEnumerable<DayOfWeek> MeetingDays;
        public TimeSpan StartTime;
        public TimeSpan EndTime;
    }
}
