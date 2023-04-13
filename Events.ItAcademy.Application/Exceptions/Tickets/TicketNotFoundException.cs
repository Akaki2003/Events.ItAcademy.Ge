using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Exceptions.Tickets
{
    public class TicketNotFoundException : Exception
    {
        public readonly string Code = "TicketIsPurchased";

        public TicketNotFoundException() : base() { }
        public TicketNotFoundException(string message) : base(message) { }
        public TicketNotFoundException(string message, Exception e) : base(message, e) { }
    }
}
