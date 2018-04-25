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

        
        public IEnumerable<Room> AvailableRooms(List<DayOfWeek> meetingDays, TimeSpan startTime, TimeSpan endTime, int minCapacity)
        {
            SearchParameters searchParameters = new SearchParameters();
            searchParameters.MeetingDays = meetingDays;
            searchParameters.StartTime = startTime;
            searchParameters.EndTime = endTime;
            searchParameters.Capacity = minCapacity;
            searchParameters.Duration = endTime - startTime;

            return ScheduleSlotsAvailable(searchParameters).ConvertAll(x => x.RoomAvailable).Distinct();
        }

        public List<ScheduleSlot> ScheduleSlotsAvailable(SearchParameters searchParameters)
        {


            var coursesGroupedByRoom = from room in roomRepository.Rooms
                                       where room.Capacity >= searchParameters.Capacity
                                       join course in courseRepository.Courses on room.RoomName equals course.RoomAssignment into courseGroup
                                       select new { Room = room, Courses = courseGroup };


            List<ScheduleSlot> availableSlots = new List<ScheduleSlot>();
            foreach (var courseGroup in coursesGroupedByRoom)
            {

                List<Course> courses = courseGroup.Courses
                    .Where(x => x.MeetingDays.Intersect(searchParameters.MeetingDays).Count(z => true) != 0 && x.StartTime.HasValue && x.StartTime.Value >= searchParameters.StartTime && x.StartTime <= searchParameters.EndTime)
                    .OrderBy(x => x.StartTime.Value)
                    .ToList();

                if (courses.Count == 0)
                {
                    availableSlots.Add(
                        new ScheduleSlot()
                        {
                            RoomAvailable = courseGroup.Room,
                            StartTime = searchParameters.StartTime,
                            EndTime = searchParameters.EndTime,
                            MeetingDays = searchParameters.MeetingDays.AsEnumerable()
                        });

                    continue;
                }

                if (courses[0].StartTime - searchParameters.StartTime >= searchParameters.Duration)
                {
                    availableSlots.Add(
                        new ScheduleSlot()
                        {
                            RoomAvailable = courseGroup.Room,
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
                                RoomAvailable = courseGroup.Room,
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
                            RoomAvailable = courseGroup.Room,
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
