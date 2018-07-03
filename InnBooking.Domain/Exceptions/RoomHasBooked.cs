using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Exceptions
{
    public class RoomHasBooked: Exception
    {
        public RoomHasBooked() : base() { }

        public RoomHasBooked(string message) : base(message) { }
    }
}
