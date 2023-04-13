using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Exceptions.Tickets
{
    public class TicketAlreadyPurchasedException : Exception
    {
        public readonly string Code = "TicketAlreadyPurchased";

        public TicketAlreadyPurchasedException() : base() { }
        public TicketAlreadyPurchasedException(string message) : base(message) { }
        public TicketAlreadyPurchasedException(string message, Exception e) : base(message, e) { }
    }
}
