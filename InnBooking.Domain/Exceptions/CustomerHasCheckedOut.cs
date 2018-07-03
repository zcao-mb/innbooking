using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Exceptions
{
    public class CustomerHasCheckedOut: Exception
    {
        public CustomerHasCheckedOut() : base() { }

        public CustomerHasCheckedOut(string message) : base(message) { }
    }
}
