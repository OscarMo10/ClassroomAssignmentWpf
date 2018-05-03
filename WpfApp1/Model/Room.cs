using ClassroomAssignment.Model.Utils;
using ClassroomAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ClassroomAssignment.Model.DataConstants;

namespace ClassroomAssignment.Model
{
    public class Room
    {
        /// <summary>
        /// Getter and setter for RoomName and Capasity of the room.
        /// </summary>
        public string RoomName { get; set; }
        public int Capacity { get; set; }

        /// <summary>
        /// Print Roomname which is room number.
        /// </summary>
        /// <returns>RoomName</returns>
        public override string ToString()
        {
            return RoomName; // RoomNumbee.
        }
    }
}
