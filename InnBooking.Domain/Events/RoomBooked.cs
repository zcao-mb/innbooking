using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Events
{
    public class RoomBooked
    {
        public Guid Id;

        public int RoomNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BookedTime { get; set; }
    }
}
