using Edument.CQRS;
using InnBooking.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Test
{
    [TestFixture]
    class StartupTest: BDDTest<RoomAggregate>
    {
        [SetUp]
        public void Setup()
        {
            BookingStartup.Setup();
        }

        [Test]
        public void CanGetRoomList()
        {
            var data = BookingStartup.BookingQueries.GetAvailableRoomList();

            Assert.NotNull(data);

            Assert.IsNotEmpty(data);
        }
    }
}
