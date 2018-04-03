using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomAssignment.Model.Repo
{
    public class InMemoryRoomRepository : IRoomRepository
    {
        private static InMemoryRoomRepository instance;
        private List<Room> myRooms;

        public List<Room> Rooms
        {
            get { return myRooms; }
            private set { myRooms = value; }
        }
        public InMemoryRoomRepository(List<Room> myRooms)
        {

            Rooms = myRooms;
        }
        public static void initInstance(List<Room> myRooms)
        {
            if (instance != null) throw new InvalidOperationException("Room Repo already initialized");
            instance = new InMemoryRoomRepository(myRooms);
        }

        public static InMemoryRoomRepository getInstance()
        {
            if (instance == null) throw new InvalidOperationException("Room Repo not yet intialized");
            return instance;
        }        

        public string GetNormalizedRoomName(string roomName)
        {
            // TODO: Placeholder implementation
            return roomName.Replace("Peter Kiewit Institute", "PKI");
        }

    }
}
