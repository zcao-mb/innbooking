using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Commands
{
    public class BookRoom
    {
        public Guid Id;

        public int RoomNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BookTime { get; set; }
    }
}
