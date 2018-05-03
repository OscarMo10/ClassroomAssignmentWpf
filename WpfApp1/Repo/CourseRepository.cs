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

namespace ClassroomAssignment.Model.Repo
{
    [Serializable]
    class CourseRepository : ObservableCourseRepository
    {
        private static CourseRepository _instance;

        private AssignmentConflictDetector roomConflictDetector;
        /// <summary>
        /// Get instance and just return that value.
        /// </summary>
        /// <returns>_instance</returns>
        public static CourseRepository GetInstance()
        {
            return _instance;
        }
        /// <summary>
        /// Handle course list exception if it is empty, 
        /// throw NullException.
        /// </summary>
        /// <param name="courses"></param>
        public static void initInstance(ICollection<Course> courses)
        {
            if (courses == null) throw new ArgumentNullException();

            _instance = new CourseRepository(courses);
            _instance.roomConflictDetector = new AssignmentConflictDetector(_instance);
        }

        private CourseRepository(ICollection<Course> courses) : base(courses)
        {
        }
        /// <summary>
        /// Get Conflicts list.
        /// </summary>
        /// <returns>AllConflicts</returns>
        public List<Conflict> GetConflicts()
        {
            return new AssignmentConflictDetector(this).AllConflicts();
        }
        /// <summary>
        /// Get conflicts involving courses.
        /// </summary>
        /// <param name="courses"></param>
        /// <returns> ConflictInvolvingCourses(course)</returns>
        public List<Conflict> GetConflictsInvolvingCourses(List<Course> courses)
        {
            return new AssignmentConflictDetector(this).ConflictsInvolvingCourses(courses);
        }

        
    }
}
