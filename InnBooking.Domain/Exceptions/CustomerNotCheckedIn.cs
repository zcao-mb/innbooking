using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Exceptions
{
    public class CustomerNotCheckedIn: Exception 
    {
        public CustomerNotCheckedIn() : base() { }

        public CustomerNotCheckedIn(string message) : base(message) { }
    }
}
