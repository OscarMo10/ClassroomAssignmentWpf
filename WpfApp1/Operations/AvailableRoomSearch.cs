using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Operations
{
    public class AvailableRoomSearch
    {

        private IRoomRepository roomRepository;
        private ICourseRepository courseRepository;


        public AvailableRoomSearch(IRoomRepository roomRepo, ICourseRepository courseRepo)
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

        public List<ScheduleSlot> ScheduleSlotsAvailable(SearchParameters searchParameters)
        {

            var coursesGroupedByRoom = from course in courseRepository.Courses
                                 where course.AlreadyAssignedRoom
                                 join room in roomRepository.Rooms on course.RoomAssignment equals room.RoomName
                                 where room.Capacity >= searchParameters.Capacity && course.StartTime.Value >= searchParameters.StartTime && course.EndTime <= searchParameters.EndTime
                                 orderby course.StartTime.Value
                                 group course by course.RoomAssignment;

            List<ScheduleSlot> availableSlots = new List<ScheduleSlot>();

            foreach (var courseGroup in coursesGroupedByRoom)
            {
                List<Course> courses = courseGroup.ToList();

                if (courses[0].StartTime - searchParameters.StartTime >= searchParameters.Duration)
                {
                    availableSlots.Add(
                        new ScheduleSlot()
                        {
                            RoomAvailable = roomRepository.GetRoomWithName(courseGroup.Key),
                            StartTime = searchParameters.StartTime,
                            EndTime = courses[0].StartTime.Value,
                            MeetingDays = searchParameters.MeetingDays.AsEnumerable()
                        });
                }

                for (int i = 0; i < courses.Count - 1; i++)
                {
                    if (courses[i + 1].StartTime - courses[i].EndTime >= searchParameters.Duration)
                    {
                        availableSlots.Add(
                            new ScheduleSlot()
                            {
                                RoomAvailable = roomRepository.GetRoomWithName(courseGroup.Key),
                                StartTime = courses[i].EndTime.Value,
                                EndTime = courses[i+1].StartTime.Value,
                                MeetingDays = searchParameters.MeetingDays.AsEnumerable()
                            });
                    }
                }

                if (searchParameters.EndTime - courses.Last().EndTime.Value  >= searchParameters.Duration)
                {
                    availableSlots.Add(
                        new ScheduleSlot()
                        {
                            RoomAvailable = roomRepository.GetRoomWithName(courseGroup.Key),
                            StartTime = courses.Last().EndTime.Value,
                            EndTime = searchParameters.EndTime,
                            MeetingDays = searchParameters.MeetingDays.AsEnumerable()
                        });
                }
            }

            
            return availableSlots;
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
