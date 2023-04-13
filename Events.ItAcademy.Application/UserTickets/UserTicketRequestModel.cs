using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.UserTickets
{
    public class UserTicketRequestModel
    {
        public int ticketId { get; set; }
        public string userId { get; set; }
        public string EventName { get; set; }
    }
}
