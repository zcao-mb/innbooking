using Edument.CQRS;
using InnBooking.Domain;
using InnBooking.Domain.Commands;
using InnBooking.Domain.Events;
using InnBooking.Domain.Exceptions;
using NUnit.Framework;
using System;

namespace InnBooking.Test
{ 
    [TestFixture]
    public class DomainTest: BDDTest<RoomAggregate>
    {
        string name = "Andy";
        string phone = "04004044040";
        string email = "test@test.com";

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CanBookARoom()
        {
            var id = Guid.NewGuid();
            int roomNumber = 101;
            var bookedTime = DateTime.Now;

            Test(
                Given(),
                When(new BookRoom
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    Name = name,
                    Phone = phone,
                    Email = email,
                    BookTime = bookedTime
                }),
                Then(new RoomBooked
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    Name = name,
                    Phone = phone,
                    Email = email,
                    BookedTime = bookedTime
                }));
        }

        [Test]
        public void CannotBookUnavailableRoom()
        {
            var id = Guid.NewGuid();
            int roomNumber = -2;

            Test(
                Given(),
                When(new BookRoom
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    Name = name,
                    BookTime = DateTime.Now
                }),
                ThenFailWith<RoomNotAvailable>());
        }

        [Test]
        public void CanCheckInBookedRoom()
        {
            var id = Guid.NewGuid();
            int roomNumber = 101;
            var checkedInTime = DateTime.Now;
            
            Test(
                Given(new RoomBooked
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    Name = name,
                    BookedTime = DateTime.Now
                }),
                When(new CheckIn
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    CheckInTime = checkedInTime
                }),
                Then(new CustomerCheckedIn
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    CheckedInTime = checkedInTime
                }));
        }

        [Test]
        public void CanCancelBookedRoom()
        {
            var id = Guid.NewGuid();
            int roomNumber = 101;
            var cancelTime = DateTime.Now;

            Test(
                Given(new RoomBooked
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    Name = name,
                    BookedTime = DateTime.Now
                }),
                When(new CancelRoom
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    CancelTime = cancelTime
                }),
                Then(new RoomCancelled
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    CancelledTime = cancelTime
                }));
        }


        [Test]
        public void CanNotCancelCheckedOutRoom()
        {
            var id = Guid.NewGuid();
            int roomNumber = 101;
            var cancelTime = DateTime.Now;

            Test(
                Given(
                    new RoomBooked
                    {
                        Id = id,
                        RoomNumber = roomNumber,
                        Name = name,
                        BookedTime = DateTime.Now
                    }, 
                    new CustomerCheckedIn
                    {
                        Id = id,
                        RoomNumber = roomNumber,
                        CheckedInTime = DateTime.Now
                    }, 
                    new CustomerCheckedOut
                    {
                        Id = id,
                        RoomNumber = roomNumber,
                        CheckedOutTime = DateTime.Now
                    }),
                When(new CancelRoom
                {
                    Id = id,
                    RoomNumber = roomNumber,
                    CancelTime = cancelTime
                }),
                ThenFailWith<CustomerHasCheckedOut>());
        }

    }
}
