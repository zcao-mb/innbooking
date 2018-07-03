using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Events
{
    public class CustomerCheckedOut
    {
        public Guid Id;
        public int RoomNumber { get; set; }
        public DateTime CheckedOutTime { get; set; }
    }
}
