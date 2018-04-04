using ClassroomAssignment.Model;
using ClassroomAssignment.Model.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.TestModels
{
    public class RoomRepo : IRoomRepository 
    {
        public List<Room> Rooms { get; }

        public RoomRepo()
        {
            this.Rooms = new List<Room>();


            Room myRoomList = new Room();

            myRoomList.RoomName = "PKI 153";
            myRoomList.MaxCapacity = 40;
            Rooms.Add(myRoomList);

            myRoomList.RoomName = "PKI 261";
            myRoomList.MaxCapacity = 56;
            Rooms.Add(myRoomList);
        }

        public string GetNormalizedRoomName(string roomName)
        {
            throw new NotImplementedException();
        }
    }

    
}
