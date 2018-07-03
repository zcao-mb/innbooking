using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Exceptions
{
    public class RoomNotAvailable: Exception
    {
        public RoomNotAvailable() : base() { }

        public RoomNotAvailable(string message) : base(message) { }
    }
}
