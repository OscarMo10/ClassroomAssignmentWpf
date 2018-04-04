using System.Collections.Generic;
using ClassroomAssignment.Model;
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
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]       
        public void RoomRepoNull_ThrowsException()
        {
            IRoomRepository roomRepo = null;
            ICourseRepository courseRepo = new NonConflictingCourseRepo();
            //RoomSearch roomSearch = new RoomSearch(roomRepo, courseRepo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ClassRepoNull_ThrowsException()
        {
            IRoomRepository roomRepo = new RoomRepo();
            ICourseRepository courseRepo = null;
            //RoomSearch roomSearch = new RoomSearch(roomRepo, courseRepo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NoRoomFound_ThrowsException()
        {
            IRoomRepository roomRepo = new RoomRepo();
            ICourseRepository courseRepo = new NonConflictingCourseRepo();
            
            //RoomSearch roomSearch = new RoomSearch(roomRepo, courseRepo);
        }

        //course repo in new method
        //create new room repo where 
        //
    }
}
