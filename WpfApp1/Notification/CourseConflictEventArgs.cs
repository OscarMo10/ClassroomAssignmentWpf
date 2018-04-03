using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignmentWpf.Notification
{
    class CourseConflictEventArgs : EventArgs
    {
        public IList<Conflict> Conflicts { get; }
        public CourseConflictEventArgs(IList<Conflict> conflicts)
        {
            Conflicts = conflicts;
        }
    }
}
