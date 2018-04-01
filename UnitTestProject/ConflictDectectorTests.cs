using ClassroomAssignment.Model.Repo;
using ClassroomAssignment.Repo;
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
            //ConflictDetector detector = new ConflictDetector(courseRepository);
            //ConflictResult result = detector.FindConflicts();

            //Assert.IsTrue(result.HasConflicts);
        }
    }
}
