using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.EventSetup;
using Events.ItAcademy.Domain.Users;
using Events.ItAcademy.Domain.UserTickets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Events.ItAcademy.Persistence.Context
{
    public class EventsItAcademyContext : IdentityDbContext<User,IdentityRole,string>
    {

        public EventsItAcademyContext(DbContextOptions<EventsItAcademyContext> options) : base(options)
        {

        }


        // DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }
        public DbSet<ArchivedEvent> ArchivedEvents { get; set; }
        public DbSet<EventSetup> EventSetups { get; set; }


        // Configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsItAcademyContext).Assembly); //stops
        }
    }
}
