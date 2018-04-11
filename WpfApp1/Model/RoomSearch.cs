using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Model
{
    public class RoomSearch
    {

        private IRoomRepository roomRepository;
        private ICourseRepository courseRepository;


        public RoomSearch(IRoomRepository roomRepo, ICourseRepository courseRepo)
        {
            roomRepository = roomRepo ?? throw new ArgumentNullException();
            courseRepository = courseRepo ?? throw new ArgumentNullException();
        }

        
        public List<Room> AvailableRooms(List<DayOfWeek> meetingDays, TimeSpan startTime, TimeSpan endTime, int minCapacity)
        {
            var possibleRooms = from room in roomRepository.Rooms
                                where room.Capacity >= minCapacity
                                select room;

           
             var coursesForRoom = from course in courseRepository.Courses
                                 where course.AlreadyAssignedRoom
                                 join room in possibleRooms on course.RoomAssignment equals room.RoomName
                                 group course by course.RoomAssignment;

            List<Room> availableRooms = new List<Room>();
            foreach(var courseGroup in coursesForRoom)
            {
                if (!CoursesConflictWithTime(courseGroup, startTime, endTime))
                {
                    availableRooms.Add(roomRepository.Rooms.Find(x => x.RoomName == courseGroup.Key));
                }
            }

            return availableRooms;

        }

        private bool CoursesConflictWithTime(IGrouping<string, Course> courseGroup, TimeSpan startTime, TimeSpan endTime)
        {
            bool hasConflict = true;

            foreach (var course in courseGroup)
            {
                if (course.EndTime < startTime)
                {
                    hasConflict = false;
                }
                else if (course.StartTime > endTime)
                {
                    hasConflict = false;
                }
            }

            return hasConflict;
        }
    }
}
