using System.Collections.Generic;
using ClassroomAssignment.Model;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassroomAssignment.Model.Repo;
using System;
using UnitTestProject.TestModels;
using ClassroomAssignment.Operations;

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
            AvailableRoomSearch roomSearch = new AvailableRoomSearch(roomRepo, courseRepo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ClassRepoNull_ThrowsException()
        {
            IRoomRepository roomRepo = new RoomRepo();
            ICourseRepository courseRepo = null;
            AvailableRoomSearch roomSearch = new AvailableRoomSearch(roomRepo, courseRepo);
        }

        [TestMethod]
        public void OnRoomAvailable()
        {
            IRoomRepository roomRepo = new RoomRepo();
            ICourseRepository courseRepo = new NonConflictingCourseRepo();

            AvailableRoomSearch roomSearch = new AvailableRoomSearch(roomRepo, courseRepo);
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
