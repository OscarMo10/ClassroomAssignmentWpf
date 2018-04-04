using ClassroomAssignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignmentWpf.Notification
{
    public class CourseConflictDetector
    {

        /// <summary>
        /// Return list of conflicts involving <paramref name="course"/> and the rest of the courses in the repo.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public List<Conflict> ConflictsInvolvingCourse(Course course)
        {
            return new List<Conflict>();
        }

        /// <summary>
        /// Finds conflicts involving the <paramref name="courses"/> and the rest of the courses in the CourseRepo
        /// </summary>
        /// <param name="courses"></param>
        /// <returns></returns>
        public List<Conflict> ConflictsInvolvingCourses(List<Course> courses)
        {
            return new List<Conflict>();
        }


        /// <summary>
        /// Return conflicts solely among the <paramref name="courses"/>
        /// </summary>
        /// <param name="courses"></param>
        /// <returns></returns>
        public List<Conflict> ConflictsAmongCourses(List<Course> courses)
        {
            return new List<Conflict>();
        }

    }
}
