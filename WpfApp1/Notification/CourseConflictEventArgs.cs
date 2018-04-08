using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Notification
{
    class CourseConflictEventArgs : EventArgs
    {
        public List<Conflict> Conflicts { get; }
        public CourseConflictEventArgs(List<Conflict> conflicts)
        {
            Conflicts = conflicts;
        }
    }
}
