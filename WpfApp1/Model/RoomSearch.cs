using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignmentWpf.Model
{
    public class RoomSearch
    {
        public RoomSearch(IRoomRepository roomRepo, ICourseRepository courseRepo )
        {
            if (roomRepo == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
