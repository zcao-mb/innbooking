using InnBooking.Domain;
using InnBooking.Domain.Commands;
using InnBooking.Domain.Events;
using InnBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnBooking.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            var bookings = BookingStartup.BookingQueries.GetCurrentBookings();
            var data = new List<BookingModel>();

            foreach(var booking in bookings)
            {
                data.Add(new BookingModel(booking));
            }
            
            return View(new BookingList() { Items = data });
        }

        [HttpPost]
        public ActionResult Change(BookingList model)
        {

            var errors = new List<string>();

            if (model.Items?.Count > 0)
            {
                var selected = model.Items.Where(item => item.Selected);
                if (selected.Count() > 0)
                {
                    var bookings = BookingStartup.BookingQueries.GetCurrentBookings();

                    foreach (var item in selected)
                    {
                        try
                        {
                            var booking = bookings.FirstOrDefault(b => b.RoomNumber == item.RoomNumber);
                            if (booking == null) continue;  //if the booking cannot be found in current booking, we just disregard it


                            if (model.Action == "checkin")
                            {
                                var command = new CheckIn { Id = booking.Id, RoomNumber = item.RoomNumber, CheckInTime = DateTime.Now };
                                BookingStartup.Dispatcher.SendCommand(command);
                            }
                            else if (model.Action == "checkout")
                            {
                                var command = new CheckOut { Id = booking.Id, RoomNumber = item.RoomNumber, CheckOutTime = DateTime.Now };
                                BookingStartup.Dispatcher.SendCommand(command);
                            }
                            else if (model.Action == "cancel")
                            {
                                var command = new CancelRoom { Id = booking.Id, RoomNumber = item.RoomNumber, CancelTime = DateTime.Now };
                                BookingStartup.Dispatcher.SendCommand(command);
                            }
                        }
                        catch(Exception ex)
                        {
                            var error = ex.GetType().Name;
                            if (!string.IsNullOrEmpty(ex.Message)) error += ": " + ex.Message;
                            errors.Add(error);
                        }
                    }
                }
            }

            if (errors.Count > 0) TempData["Errors"] = errors;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Rooms()
        {
            var rooms = BookingStartup.BookingQueries.GetAvailableRoomList();
            var data = new List<RoomModel>();

            if (rooms != null)
                foreach (var room in rooms)
                    data.Add(new RoomModel(room));

            return View(data);
        }

        [HttpGet]
        public ActionResult Book(int id)
        {
            var rooms = BookingStartup.BookingQueries.GetAvailableRoomList();
            var room = rooms.FirstOrDefault(r => r.RoomNumber == id);

            var booking = new BookingModel() { RoomNumber = room.RoomNumber, RoomDescription = room.Description };

            return View(booking);
        }

        [HttpPost]
        public ActionResult Book(BookingModel model)
        {
            if(ModelState.IsValid)
            {
                var book = new BookRoom
                {
                    Id = Guid.NewGuid(),
                    RoomNumber = model.RoomNumber,
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    BookTime = DateTime.Now
                };
                BookingStartup.Dispatcher.SendCommand(book);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }


}