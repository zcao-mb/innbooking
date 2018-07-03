using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Exceptions
{
    public class CustomerHasCheckedIn: Exception
    {
        public CustomerHasCheckedIn() : base() { }

        public CustomerHasCheckedIn(string message) : base(message) { }
    }
}
