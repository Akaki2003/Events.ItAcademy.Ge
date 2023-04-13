using Events.ItAcademy.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.ItAcademy.Domain.UserTickets;

namespace Events.ItAcademy.Persistence.Configurations
{
    internal class UserTicketConfiguration : IEntityTypeConfiguration<UserTicket>
    {
        public void Configure(EntityTypeBuilder<UserTicket> builder)
        {
            builder.HasKey(x => x.TicketId);
            builder.HasOne(x => x.User).WithMany(x => x.Tickets).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Event).WithMany(x => x.Tickets).HasForeignKey(x => x.EventId);
            //builder.HasOne(x => x.User).WithMany(x => x.Tickets)
            //    .OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(x => x.Event).WithMany(x => x.Tickets)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
