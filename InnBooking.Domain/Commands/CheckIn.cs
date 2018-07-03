using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Commands
{
    public class CheckIn
    {
        public Guid Id;
        public int RoomNumber { get; set; }
        public DateTime CheckInTime { get; set; }
    }
}
