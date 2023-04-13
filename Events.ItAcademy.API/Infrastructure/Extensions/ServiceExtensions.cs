using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Application.Users;
using Events.ItAcademy.Application.Users.Repositories;
using Events.ItAcademy.Infrastructure.Users;
using Events.ItAcademy.Infrastructure.Events;
using Events.ItAcademy.Application.Roles;
using Events.ItAcademy.Application.Roles.Repositories;
using Events.ItAcademy.Infrastructure.Roles;
using Events.ItAcademy.Application.ArchivedEvents;
using Events.ItAcademy.Application.ArchivedEvents.Repositories;
using Events.ItAcademy.Infrastructure.ArchivedEvents;
using Events.ItAcademy.Infrastructure.UserTickets;
using Events.ItAcademy.Application.UserTickets;
using Events.ItAcademy.Application.UserTickets.Repositories;
using Events.ItAcademy.Application.EventsSetups.Repositories;
using Events.ItAcademy.Application.EventsSetups;
using Events.ItAcademy.Infrastructure.EventSetups;

namespace Events.ItAcademy.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserTicketService, UserTicketService>();
            services.AddScoped<IEventSetupService, EventSetupService>();

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserTicketRepository, UserTicketRepository>();
            services.AddScoped<IEventSetupRepository, EventSetupRepository>();


            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


        }
    }
}
