using ClassroomAssignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Util
{
    class TestUtil
    {
        static Course CreateTestCourseWithoutDerivedProperties()
        {
            Course testCourse = new Course();
            testCourse.ClassID = "239";
            testCourse.SIS_ID = "12615";
            testCourse.TermCode = "1188";
            testCourse.DepartmentCode = "UNO-BIOI";
            testCourse.SubjectCode = "BIOI";
            testCourse.CatalogNumber = "1000";
            testCourse.CourseName = "BIOI 1000";
            testCourse.CourseTitle = "BIOI 1000";
            testCourse.SectionNumber = "1";
            testCourse.MeetingPattern = "MW 1:30pm-2:45pm";
            testCourse.Instructor = "Bastola, Dhundy";
            testCourse.Room = "General Assignment Room";
            testCourse.Status = "Active";
            testCourse.Session = "Regular Academic Session";
            testCourse.Campus = "UNO";
            testCourse.InstructionMethod = "In Person";
            testCourse.Comments = "PKI 153";
            testCourse.Notes = "PKI 157";
            testCourse.AlreadyAssignedRoom = true;

            return testCourse;
        }
    }
}
