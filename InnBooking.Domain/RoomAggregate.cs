using System;
using System.Collections;
using System.Collections.Generic;
using Edument.CQRS;
using InnBooking.Domain.Commands;
using InnBooking.Domain.Events;
using InnBooking.Domain.Exceptions;

namespace InnBooking.Domain
{
    public class RoomAggregate : Aggregate,
                    IHandleCommand<BookRoom>,
                    IHandleCommand<CheckIn>,
                    IHandleCommand<CheckOut>,
                    IHandleCommand<CancelRoom>,
                    IApplyEvent<RoomBooked>,
                    IApplyEvent<CustomerCheckedIn>,
                    IApplyEvent<CustomerCheckedOut>,
                    IApplyEvent<RoomCancelled>
    {
        private RoomStatus status = RoomStatus.Available;

        public void Apply(RoomBooked e)
        {
            status = RoomStatus.Booked;
        }

        public void Apply(CustomerCheckedIn e)
        {
            status = RoomStatus.CheckedIn;
        }

        public void Apply(CustomerCheckedOut e)
        {
            status = RoomStatus.CheckedOut;
        }

        public void Apply(RoomCancelled e)
        {
            status = RoomStatus.Available;
        }

        public IEnumerable Handle(BookRoom c)
        {
            // we not accept a zero or negative room number 
            if (c.RoomNumber < 1) throw new RoomNotAvailable($"The room {c.RoomNumber} is not avaialbe for booking.");
            if (status != RoomStatus.Available) throw new RoomHasBooked($"The room {c.RoomNumber} has been booked.");

            yield return new RoomBooked
            {
                Id = c.Id,
                RoomNumber = c.RoomNumber,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                BookedTime = c.BookTime
            };
        }

        public IEnumerable Handle(CheckIn c)
        {
            if (status != RoomStatus.Booked)
            {
                if (status == RoomStatus.CheckedIn)
                    throw new CustomerHasCheckedIn($"Customer has already checked in room {c.RoomNumber}");
                else if (status == RoomStatus.CheckedOut)
                    throw new CustomerHasCheckedOut($"Customer has already checked out room {c.RoomNumber}");
                else 
                    throw new RoomNotBooked($"The room {c.RoomNumber} has not been booked.");
            }
            yield return new CustomerCheckedIn
            {
                Id = c.Id,
                RoomNumber = c.RoomNumber,
                CheckedInTime = c.CheckInTime
            };
        }

        public IEnumerable Handle(CheckOut c)
        {
            if (status != RoomStatus.CheckedIn) throw new CustomerNotCheckedIn($"Customer has not checked in the room {c.RoomNumber}.");

            yield return new CustomerCheckedOut
            {
                Id = c.Id,
                RoomNumber = c.RoomNumber,
                CheckedOutTime = c.CheckOutTime
            };
        }

        public IEnumerable Handle(CancelRoom c)
        {
            if (status == RoomStatus.Available) throw new RoomNotBooked($"The room {c.RoomNumber} has not been booked.");
            if (status == RoomStatus.CheckedOut) throw new CustomerHasCheckedOut($"Customer has already checked out the room {c.RoomNumber}. Cancellation is not allowed.");

            yield return new RoomCancelled
            {
                Id = c.Id,
                RoomNumber = c.RoomNumber,
                CancelledTime = c.CancelTime
            };
        }
    }
}
