using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassroomAssignment.Model.Repo
{
    [Serializable]
    public class RoomRepository : IRoomRepository
    {
        private static RoomRepository instance;

        private List<Room> _rooms;

        public List<Room> Rooms { get; private set; }

        public RoomRepository()
        {

            Rooms = AllRooms();
        }
        public static void InitInstance()
        {
            if (instance != null) throw new InvalidOperationException("Room Repo already initialized");
            instance = new RoomRepository();
        }

        public static RoomRepository GetInstance()
        {
            return instance ?? throw new InvalidOperationException("Room Repo not yet intialized");
           
        }    
        
        public List<Room> AllRooms()
        {
            List<Room> rooms = new List<Room>();

            rooms.Add(new Room() { RoomName = "PKI 153", Capacity = 40 });
            rooms.Add(new Room() { RoomName = "PKI 155", Capacity = 45 });
            rooms.Add(new Room() { RoomName = "PKI 157", Capacity = 24 });
            rooms.Add(new Room() { RoomName = "PKI 160", Capacity = 44 });
            rooms.Add(new Room() { RoomName = "PKI 161", Capacity = 30 });
            rooms.Add(new Room() { RoomName = "PKI 164", Capacity = 56 });
            rooms.Add(new Room() { RoomName = "PKI 252", Capacity = 58 });
            rooms.Add(new Room() { RoomName = "PKI 256", Capacity = 40 });
            rooms.Add(new Room() { RoomName = "PKI 259", Capacity = 20 });
            rooms.Add(new Room() { RoomName = "PKI 260", Capacity = 40 });
            rooms.Add(new Room() { RoomName = "PKI 261", Capacity = 24 });
            rooms.Add(new Room() { RoomName = "PKI 263", Capacity = 48 });
            rooms.Add(new Room() { RoomName = "PKI 269", Capacity = 30 });
            rooms.Add(new Room() { RoomName = "PKI 270", Capacity = 16 });
            rooms.Add(new Room() { RoomName = "PKI 274", Capacity = 30 });
            rooms.Add(new Room() { RoomName = "PKI 276", Capacity = 35 });
            rooms.Add(new Room() { RoomName = "PKI 278", Capacity = 35 });
            rooms.Add(new Room() { RoomName = "PKI 279", Capacity = 30 });
            rooms.Add(new Room() { RoomName = "PKI 361", Capacity = 35 });


            return rooms;
        }

        public Room GetRoomWithName(string roomName)
        {
            return Rooms.Find(x => x.RoomName == roomName);
        }

        public string GetNormalizedRoomName(string roomName)
        {
            // TODO: Placeholder implementation
            return roomName.Replace("Peter Kiewit Institute", "PKI");
        }

    }
}
