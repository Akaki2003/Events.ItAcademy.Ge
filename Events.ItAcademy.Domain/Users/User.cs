using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.UserTickets;
using Microsoft.AspNetCore.Identity;

namespace Events.ItAcademy.Domain.Users
{
    public class User : IdentityUser
    {

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;


        public List<Event> Events { get; set; }
        public List<UserTicket> Tickets { get; set; }
        public List<ArchivedEvent> ArchivedEvents { get; set; }
    }

    public class UserRole : IdentityRole
    {

    }
}
