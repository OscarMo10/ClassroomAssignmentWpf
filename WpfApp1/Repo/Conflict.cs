using ClassroomAssignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Repo
{
    public class Conflict
    {
        public List<Course> ConflictingCourses { get; }

        public Conflict(List<Course> conflictingCourses)
        {
            ConflictingCourses = conflictingCourses;
        }

        public string Description
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                foreach (var course in ConflictingCourses)
                {
                    builder.Append(course.ClassID);
                    builder.Append(", ");
                }

                builder.Remove(builder.Length - 2, 2);
                builder.Append(" Are In Conflict.");

                return builder.ToString();
            }
        }
    }
}
