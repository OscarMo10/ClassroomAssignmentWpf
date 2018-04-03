using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignmentWpf.Model
{
    public class RoomSearch
    {

        //hardcoded repos
        public  HardCodedCourseRepo myhardCodedCourseRepo = new HardCodedCourseRepo();
        public static HardCodedRoomRepo hardCodedRoomRepo = new HardCodedRoomRepo();

        //list with all the courses
        public static List<Course> courseListing;

        //list with all the hardcoded rooms
        public static List<Room> roomListing = hardCodedRoomRepo.Rooms;

        //Room search results
        public List<Room> roomSearchResults;

        public IRoomRepository myNewRoomRepo = new InMemoryRoomRepository(roomListing);
        public ICourseRepository myCoursesRepo = new CourseRepository(courseListing);

        public RoomSearch(IRoomRepository roomRepo, ICourseRepository courseRepo )
        {
            if (roomRepo != null )
            {
                myNewRoomRepo = roomRepo;
                

            }
            else
            {
                throw new ArgumentNullException();
            }

            if(courseRepo != null)
            {
                myCoursesRepo = courseRepo;
            }
            else
            {
                throw new ArgumentNullException();
            }

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
        public void searchRooms()
        {
            foreach (var x in myNewRoomRepo.Rooms)
            {
               foreach(var y in myCoursesRepo.Courses)
                {
                    if(y.AlreadyAssignedRoom == false)
                    {
                        
                    }
                }
            }
            
        }

    }
}
