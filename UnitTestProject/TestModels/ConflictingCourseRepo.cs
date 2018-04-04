using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.TestModels
{
    class ConflictingCourseRepo : ICourseRepository
    {
        private List<Course> _courses = new List<Course>();

        public List<Course> Courses => _courses;

        public ConflictingCourseRepo()
        {
            AddCourse1();
            AddCourse2();
        }

        public void AddCourse1()
        {
            Course testCourse = new Course();
            testCourse.ClassID = "239";
            testCourse.SIS_ID = "12615";
            testCourse.TermCode = "1188";
            testCourse.DepartmentCode = "UNO-BIOI";
            testCourse.SubjectCode = "BIOI";
            testCourse.CatalogNumber = "1000";
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
            testCourse.StartTime = new TimeSpan(13, 30, 0);
            testCourse.EndTime = new TimeSpan(14, 45, 0);

            // This is the "normalized" name for the room
            testCourse.RoomAssignment = "PKI 157";
            testCourse.MeetingDays = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Wednesday };

            Courses.Add(testCourse);
        }

        public void AddCourse2()
        {
            Course testCourse = new Course();
            testCourse.ClassID = "139";
            testCourse.SIS_ID = "12134";
            testCourse.TermCode = "1188";
            testCourse.DepartmentCode = "UNO-BIOI";
            testCourse.SubjectCode = "BIOI";
            testCourse.CatalogNumber = "1001";
            testCourse.CourseTitle = "BIOI 1001";
            testCourse.SectionNumber = "1";
            testCourse.MeetingPattern = "MW 1:45pm-3:00pm";
            testCourse.Instructor = "Dandy, Maley";
            testCourse.Room = "General Assignment Room";
            testCourse.Status = "Active";
            testCourse.Session = "Regular Academic Session";
            testCourse.Campus = "UNO";
            testCourse.InstructionMethod = "In Person";
            testCourse.Comments = "";
            testCourse.Notes = "PKI 157";
            testCourse.SetDerivedProperties();

            Courses.Add(testCourse);
        }

    }
}
