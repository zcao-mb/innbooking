using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnBooking.Domain.Exceptions
{
    public class CustomerNotCheckedOut: Exception
    {
        public CustomerNotCheckedOut() : base() { }

        public CustomerNotCheckedOut(string message) : base(message) { }
    }
}
