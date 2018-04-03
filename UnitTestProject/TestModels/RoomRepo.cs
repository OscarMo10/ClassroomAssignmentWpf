﻿using ClassroomAssignment.Model;
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

            myRoomList.roomName = "PKI 153";
            myRoomList.maxCapcity = 40;
            Rooms.Add(myRoomList);

            myRoomList.roomName = "PKI 261";
            myRoomList.maxCapcity = 56;
            Rooms.Add(myRoomList);
        }

        public string GetNormalizedRoomName(string roomName)
        {
            throw new NotImplementedException();
        }
    }

    
}
