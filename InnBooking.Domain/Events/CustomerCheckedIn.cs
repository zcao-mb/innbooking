using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Events
{
    public class CustomerCheckedIn
    {
        public Guid Id;
        public int RoomNumber { get; set; }
        public DateTime CheckedInTime { get; set; }
    }
}
