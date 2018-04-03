using System.Collections.Generic;
using ClassroomAssignment.Model;
using ClassroomAssignment.Repo;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassroomAssignment.Model.Repo;
using System;
using UnitTestProject.TestModels;
using ClassroomAssignmentWpf.Model;

namespace UnitTestProject
{
    [TestClass]
    public class RoomSearchTest
    {
        //hardcoded repos
        private HardCodedCourseRepo myhardCodedCourseRepo = new HardCodedCourseRepo();
        public static HardCodedRoomRepo hardCodedRoomRepo = new HardCodedRoomRepo();


        //list with all the courses
        public List<Course> courseListing;

        //list with all the hardcoded rooms
        private List<Room> roomListing = hardCodedRoomRepo.Rooms;

        //Room search results
        public List<Room> roomSearchResults;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]       
        public void RoomRepoNull_ThrowsException()
        {
            IRoomRepository roomRepo = null;
            ICourseRepository courseRepo = new NonConflictingCourseRepo();
            RoomSearch roomSearch = new RoomSearch(roomRepo, courseRepo);

           
          
        }

        //course repo in new method
    }
}
