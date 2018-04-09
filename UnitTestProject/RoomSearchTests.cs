using System.Collections.Generic;
using ClassroomAssignment.Model;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassroomAssignment.Model.Repo;
using System;
using UnitTestProject.TestModels;


namespace UnitTestProject
{
    [TestClass]
    public class RoomSearchTests
    {
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]       
        public void RoomRepoNull_ThrowsException()
        {
            IRoomRepository roomRepo = null;
            ICourseRepository courseRepo = new NonConflictingCourseRepo();
            RoomSearch roomSearch = new RoomSearch(roomRepo, courseRepo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ClassRepoNull_ThrowsException()
        {
            IRoomRepository roomRepo = new RoomRepo();
            ICourseRepository courseRepo = null;
            RoomSearch roomSearch = new RoomSearch(roomRepo, courseRepo);
        }

        [TestMethod]
        public void OnRoomAvailable()
        {
            IRoomRepository roomRepo = new RoomRepo();
            ICourseRepository courseRepo = new NonConflictingCourseRepo();

            RoomSearch roomSearch = new RoomSearch(roomRepo, courseRepo);
            var meetingDays = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Wednesday };
            var startingTime = new TimeSpan(12, 0, 0);
            var endingTime = new TimeSpan(13, 35, 0);
            var minCapacity = 40;
            var availableRooms = roomSearch.AvailableRooms(meetingDays, startingTime, endingTime, minCapacity);

            Assert.AreEqual<int>(1, availableRooms.Count);
            Assert.AreEqual<string>("PKI 158", availableRooms[0].RoomName);

        }

      

      
    }
}
