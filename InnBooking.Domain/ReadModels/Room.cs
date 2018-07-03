using InnBooking.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.ReadModels
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public string Description { get; set; }
    }
}
