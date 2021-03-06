﻿using ClassroomAssignment.Model.Utils;
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
            var result = room != null &&
                   RoomName == room.RoomName &&
                   Capacity == room.Capacity;

            return result;
        }

        public override int GetHashCode()
        {
            var hashCode = 1430268434;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RoomName);
            hashCode = hashCode * -1521134295 + Capacity.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return RoomName; // RoomNumbee.
        }

        public static bool operator ==(Room room1, Room room2)
        {
            return EqualityComparer<Room>.Default.Equals(room1, room2);
        }

        public static bool operator !=(Room room1, Room room2)
        {
            return !(room1 == room2);
        }
    }
}
