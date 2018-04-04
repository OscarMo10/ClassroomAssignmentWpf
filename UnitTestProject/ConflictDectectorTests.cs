using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Repo;
using ClassroomAssignmentWpf.Notification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject.TestModels;

namespace UnitTestProject
{
    [TestClass]
    class ConflictDetectorTests
    {
        [TestMethod]
        public void HasConflict_True()
        {
            ICourseRepository coureRepository = new ConflictingCourseRepo();
            CourseConflictDetector detector = new CourseConflictDetector(courseRepository);
            List<Conflict> conflicts = detector.ConflictsInvolvingCourse(course);

            //Assert.IsTrue(result.HasConflicts);
        }

        [TestMethod]
        public void HasConflict_False()
        {
            /*
            ICourseRepository courseRepository = new NonConflictingCourseRepo();
            ConflictDetectorTests detector = new ConflictDetector(courseRepository);
            ConflictResult result = detector.FindConflicts();

            Assert.IsFalse(result.HasConflicts);
            */
        }

       

    }
}
