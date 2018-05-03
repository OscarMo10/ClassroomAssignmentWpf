using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Model.Repo
{
    /// <summary>
    /// Getter for Courses list.
    /// </summary>
    public interface ICourseRepository
    {
        IList<Course> Courses { get; }
    }
}
