using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.ReadModels
{
    public class Booking
    {
        public Guid Id { get; set; }
        public int RoomNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BookedTime { get; set; }
        public DateTime? CheckedInTime { get; set; }
        public DateTime? CheckedOutTime { get; set; }
        public DateTime? CancelledTime { get; set; }
        public BookingStatus Status { get; set; }
    }

    public enum BookingStatus
    {
        Booked,
        CheckedIn,
        CheckedOut,
        Cannceled,
        Completed
    }
}
