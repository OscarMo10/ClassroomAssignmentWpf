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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using ClassroomAssignment.Model;

namespace ClassroomAssignment.Repo
{
    [Serializable]
    class CourseRepository : ICourseRepository
    {
        public IEnumerable<Course> Courses { get; }

        private static CourseRepository _instance;
        private AssignmentConflictDetector roomConflictDetector;
        private List<Conflict> CachedAllConflicts;

        public event EventHandler<ChangeInConflictsEventArgs> ChangeInConflicts;

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

        private CourseRepository(IEnumerable<Course> courses)
        {
            Courses = courses;
        }

        public List<Conflict> GetConflicts()
        {
            if (CachedAllConflicts == null)
            {
                CachedAllConflicts =  new AssignmentConflictDetector(this).AllConflicts();
            }


            return CachedAllConflicts;
        }

        public List<Conflict> GetConflictsInvolvingCourses(List<Course> courses)
        {
            return new AssignmentConflictDetector(this).ConflictsInvolvingCourses(courses);
        }


        public class ChangeInConflictsEventArgs : EventArgs
        {
            public IEnumerable<Conflict> Conflicts { get; set; }
        }

    }
}
