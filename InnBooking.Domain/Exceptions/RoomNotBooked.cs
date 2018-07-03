using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Exceptions
{
    public class RoomNotBooked: Exception
    {
        public RoomNotBooked() : base() { }

        public RoomNotBooked(string message) : base(message) { }
    }
}
