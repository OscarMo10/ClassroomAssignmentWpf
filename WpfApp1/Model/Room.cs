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
    [Serializable]
    public class Room
    {
        /// <summary>
        /// Getter and setter for RoomName and Capasity of the room.
        /// </summary>
        public string RoomName { get; set; }
        public int Capacity { get; set; }

        public override bool Equals(object obj)
        {
            var room = obj as Room;
            return room != null &&
                   RoomName == room.RoomName &&
                   Capacity == room.Capacity;
        }

        public override int GetHashCode()
        {
            var hashCode = 1430268434;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RoomName);
            hashCode = hashCode * -1521134295 + Capacity.GetHashCode();
            return hashCode;
        }

        public static bool operator==(Room a, Room b)
        {
            return  (a?.RoomName == b?.RoomName) && (a?.Capacity == b?.Capacity);
        }

        public static bool operator!=(Room a, Room b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return RoomName; // RoomNumbee.
        }


    }
}
