using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignmentWpf.Model
{
    public class RoomSearch
    {

        ////hardcoded repos
        //public  HardCodedCourseRepo myhardCodedCourseRepo = new HardCodedCourseRepo();
        //public static HardCodedRoomRepo hardCodedRoomRepo = new HardCodedRoomRepo();

        //list with all the courses
        public static List<Course> courseListing;

        //list with all the hardcoded rooms
        public static List<Room> roomListing = hardCodedRoomRepo.Rooms;

        //Room search results
        public List<Room> roomSearchResults;

        private IRoomRepository roomRepository;
        private ICourseRepository courseRepository;
        private RoomSearch roomSearch; 
        

        public RoomSearch(IRoomRepository roomRepo, ICourseRepository courseRepo, RoomSearch roomSearch)
        {
            if (roomRepo == null) throw new ArgumentNullException();
            if (courseRepo == null) throw new ArgumentNullException();

            this.roomSearch = roomSearch;

            
        }

        /* Takes List<>rmSearch 
         * searchRooms takes a room list and courseListing 
         * 
         * go down list of rooms, check max seating for each room not more than max of room
         * if mySelectedCourse doesn't have a room assigned
         *  then go through list of currentCourses with assigned room, time and days
         *      for current room
         *          if ( (currentCourse.startTime <= mySelectedCourse.startTime <= currentCourse.endTime) || (currentCourse.StartTime <= mySelectedCourse.EndTime <= currentCourse.EndTime) )
         *              check next room in room List
         *          else
         *              check next course in currentCourses
         *              
         *         
         *
          */ 
        public List<Room> AvailableRooms(List<DayOfWeek> meetingDays, TimeSpan startTime, TimeSpan endTime, int minCapacity)
        {
            var possibleRooms = roomRepository.Rooms.FindAll(m => m.MaxCapacity >= minCapacity);

            var possibleConflictingCourses =
               courseRepository.Courses.FindAll(m => m.AlreadyAssignedRoom && possibleRooms.Any(x => x.RoomName == m.RoomAssignment));

            var coursesForRoom = from course in possibleConflictingCourses
                                 group course by course.RoomAssignment;


            Course assignmentCourse = new Course();
            assignmentCourse.StartTime = startTime;
            assignmentCourse.EndTime = endTime;
            assignmentCourse.MeetingDays = meetingDays;


            List<string> availableRoomNames = new List<string>();
            foreach (var roomGroup in coursesForRoom)
            {
                assignmentCourse.RoomAssignment = roomGroup.Key;
                List<Conflict> conflicts = roomConflictFinder.ConflictAmongCourses(roomGroup.ToList().Add(assignmentCourse));
                if (conflicts.Count == 0) availableRoomNames.Add(roomGroup.Key);
            }

            var availableRooms = from room in roomRepository.Rooms
                                 where availableRoomNames.Contains(room.RoomName)
                                 select room;

            return availableRooms.ToList();
        }

    }
}
