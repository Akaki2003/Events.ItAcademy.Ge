using Events.ItAcademy.Domain.Users;
using Events.ItAcademy.Domain.UserTickets;

namespace Events.ItAcademy.Domain.Events
{
    public class Event
    {
        public int Id { get; set; }
        public Event()
        {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int TicketCount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PhotoPath { get; set; }

        public User User { get; set; }
        public List<UserTicket> Tickets { get; set; }
    }
}
