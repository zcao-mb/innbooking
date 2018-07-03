using Edument.CQRS;
using InnBooking.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.ReadModels
{
    public class BookingList : IBookingQueries,
        ISubscribeTo<RoomBooked>,
        ISubscribeTo<CustomerCheckedIn>,
        ISubscribeTo<CustomerCheckedOut>,
        ISubscribeTo<RoomCancelled>
    {
        private IList<Room> roomList = new List<Room>();
        private IList<Booking> currentBookings = new List<Booking>();

        public BookingList()
        {
            roomList.Add(new Room { RoomNumber = 101, Description = "Standard" });
            roomList.Add(new Room { RoomNumber = 102, Description = "Standard" });
            roomList.Add(new Room { RoomNumber = 103, Description = "Double" });
            roomList.Add(new Room { RoomNumber = 104, Description = "Double" });
            roomList.Add(new Room { RoomNumber = 105, Description = "Double" });
            roomList.Add(new Room { RoomNumber = 201, Description = "Single" });
            roomList.Add(new Room { RoomNumber = 202, Description = "Single" });
            roomList.Add(new Room { RoomNumber = 203, Description = "Double" });
            roomList.Add(new Room { RoomNumber = 204, Description = "Double" });
            roomList.Add(new Room { RoomNumber = 205, Description = "Single" });
        }

        public IEnumerable<Room> GetAvailableRoomList()
        {
            lock (currentBookings)
            {
                var booked = currentBookings.Select(b => b.RoomNumber);
                return roomList.Where(room => !booked.Contains(room.RoomNumber));
            }
        }

        public IEnumerable<Booking> GetCurrentBookings()
        {
            lock (currentBookings)
                return currentBookings.ToList();
        }

        public void Handle(RoomBooked e)
        {
            lock (currentBookings)
            {
                currentBookings.Add(new Booking
                {
                    Id = e.Id,
                    RoomNumber = e.RoomNumber,
                    Name = e.Name,
                    Phone = e.Phone,
                    Email = e.Email,
                    BookedTime = e.BookedTime,
                    Status = BookingStatus.Booked
                });
            }
        }

        public void Handle(CustomerCheckedIn e)
        {
            lock(currentBookings)
            {
                var booking = currentBookings.FirstOrDefault(b => b.Id == e.Id);
                if(booking != null)
                {
                    booking.Status = BookingStatus.CheckedIn;
                    booking.CheckedInTime = e.CheckedInTime;
                }
            }
        }

        public void Handle(CustomerCheckedOut e)
        {
            lock (currentBookings)
            {
                var booking = currentBookings.FirstOrDefault(b => b.Id == e.Id);
                if (booking != null)
                {
                    booking.Status = BookingStatus.CheckedOut;
                    booking.CheckedOutTime = e.CheckedOutTime;
                }
            }
        }

        public void Handle(RoomCancelled e)
        {
            lock (currentBookings)
            {
                var booking = currentBookings.FirstOrDefault(b => b.Id == e.Id);
                if (booking != null)
                {
                    booking.Status = BookingStatus.Cannceled;
                    booking.CancelledTime = e.CancelledTime;
                }
            }
        }
    }
}
