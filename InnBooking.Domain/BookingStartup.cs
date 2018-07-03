using Edument.CQRS;
using InnBooking.Domain;
using InnBooking.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnBooking.Domain
{
    public static class BookingStartup
    {
        public static MessageDispatcher Dispatcher;
        public static IBookingQueries BookingQueries; 

        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new InMemoryEventStore());

            Dispatcher.ScanInstance(new RoomAggregate());

            BookingQueries = new BookingList();
            Dispatcher.ScanInstance(BookingQueries);
        }
    }
}