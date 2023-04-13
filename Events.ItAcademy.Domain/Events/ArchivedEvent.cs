using Events.ItAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Domain.Events
{
    public class ArchivedEvent
    {
        public int Id { get; set; }
        public ArchivedEvent()
        {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int TicketCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public User User { get; set; }
    }
}
