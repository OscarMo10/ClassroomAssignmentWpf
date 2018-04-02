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

        public static void initInstance()
        {
            if (instance != null) throw new InvalidOperationException("Room Repo already initialized");
            instance = new InMemoryRoomRepository();
        }

        public static InMemoryRoomRepository getInstance()
        {
            if (instance == null) throw new InvalidOperationException("Room Repo not yet intialized");
            return instance;
        }        

        public string getNormalizedRoomName(string roomName)
        {
            // TODO: Placeholder implementation
            return roomName.Replace("Peter Kiewit Institute", "PKI");
        }

    }
}
