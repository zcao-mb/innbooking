using InnBooking.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnBooking.Models
{
    public class RoomModel
    {
        public int RoomNumber { get; set; }
        public string RoomDescription { get; set; }

        public RoomModel(Room room)
        {
            this.RoomNumber = room.RoomNumber;
            this.RoomDescription = room.Description;
        }
    }
}