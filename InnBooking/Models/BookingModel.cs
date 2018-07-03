using InnBooking.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InnBooking.Models
{
    public class BookingList
    {
        public List<BookingModel> Items { get; set; }
        public string Action { get; set; }
    }

    public class BookingModel
    {
        [Required]
        public int RoomNumber { get; set; }
        public string RoomDescription { get; set; }

        [Required(ErrorMessage ="Please enter the customer name")]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public BookingStatus Status { get; set; }
        public DateTime BookedTime { get; set; }
        public DateTime? CheckedInTime { get; set; }
        public DateTime? CheckedOutTime { get; set; }
        public DateTime? CancelledTime { get; set; }

        public bool Selected { get; set; }

        public BookingModel()
        {

        }

        public BookingModel(Booking booking)
        {
            this.RoomNumber = booking.RoomNumber;
            this.Name = booking.Name;
            this.Phone = booking.Phone;
            this.Email = booking.Email;
            this.Status = booking.Status;

            this.BookedTime = booking.BookedTime;
            this.CheckedInTime = booking.CheckedInTime;
            this.CheckedOutTime = booking.CheckedOutTime;
            this.CancelledTime = booking.CancelledTime;

            this.Selected = false;
        }
    }
}