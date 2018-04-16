using ClassroomAssignment.Notification;
using ClassroomAssignment.Operations;
using ClassroomAssignment.Repo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Model.Repo
{
    class CourseRepository : ObservableCourseRepository
    {
        private static CourseRepository _instance;

        private AssignmentConflictDetector roomConflictDetector;

        public static CourseRepository GetInstance()
        {
            return _instance;
        }

        public static void initInstance(ICollection<Course> courses)
        {
            if (courses == null) throw new ArgumentNullException();

            _instance = new CourseRepository(courses);
            _instance.roomConflictDetector = new AssignmentConflictDetector(_instance);
        }

        private CourseRepository(ICollection<Course> courses) : base(courses)
        {
        }

        public List<Conflict> GetConflicts()
        {
            return new AssignmentConflictDetector(this).AllConflicts();
        }

        public List<Conflict> GetConflictsInvolvingCourses(List<Course> courses)
        {
            return new AssignmentConflictDetector(this).ConflictsInvolvingCourses(courses);
        }

        
    }
}
