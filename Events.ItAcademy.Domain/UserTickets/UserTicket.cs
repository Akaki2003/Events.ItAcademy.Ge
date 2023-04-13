using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.Users;

namespace Events.ItAcademy.Domain.UserTickets
{
    public class UserTicket
    {
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public bool Reserved { get; set; }
        public DateTime? ReservationDate { get; set; }



        public User User { get; set; }
        public Event Event { get; set; }
    }
}
