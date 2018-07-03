using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Commands
{
    public class CheckOut
    {
        public Guid Id;
        public int RoomNumber { get; set; }
        public DateTime CheckOutTime { get; set; }
    }
}
