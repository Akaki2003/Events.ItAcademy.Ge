using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Persistence.Configurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(@event => @event.Id);
            builder.HasOne(@event => @event.User).WithMany(user => user.Events);
            builder.Property(@event => @event.Title).IsRequired().HasMaxLength(100);
            builder.Property(@event => @event.Description).IsRequired().HasMaxLength(300);
            builder.Property(@event => @event.Price).IsRequired();
            builder.Property(@event => @event.TicketCount).IsRequired();
            builder.Property(@event => @event.IsActive).IsRequired();
            builder.Property(@event => @event.PhotoPath).IsRequired(false);
            builder.Property(@event => @event.StartDate).IsRequired().HasColumnType("datetime");
            builder.Property(@event => @event.EndDate).IsRequired().HasColumnType("datetime");
            builder.Property(@event => @event.CreatedAt).IsRequired().HasColumnType("datetime");
            builder.Property(@event => @event.ModifiedAt).IsRequired().HasColumnType("datetime");
            builder.HasOne(@event => @event.User)
       .WithMany(user => user.Events)
       .HasForeignKey(@event => @event.UserId)
       .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
