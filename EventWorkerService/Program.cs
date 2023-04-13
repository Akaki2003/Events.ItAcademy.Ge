using Events.ItAcademy.Application.ArchivedEvents;
using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.EventsSetups.Repositories;
using Events.ItAcademy.Application.EventsSetups;
using Events.ItAcademy.Application.Roles.Repositories;
using Events.ItAcademy.Application.Roles;
using Events.ItAcademy.Application.Users.Repositories;
using Events.ItAcademy.Application.Users;
using Events.ItAcademy.Application.UserTickets.Repositories;
using Events.ItAcademy.Application.UserTickets;
using EventWorkerService;
using Microsoft.AspNetCore.Http;
using Serilog;
using Events.ItAcademy.Infrastructure.Events;
using Events.ItAcademy.Infrastructure.Users;
using Events.ItAcademy.Infrastructure.Roles;
using Events.ItAcademy.Infrastructure.UserTickets;
using Events.ItAcademy.Infrastructure.EventSetups;
using Events.ItAcademy.Persistence;
using Events.ItAcademy.Infrastructure;
using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.UserTickets;
using Events.ItAcademy.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Events.ItAcademy.Application.ArchivedEvents.Repositories;
using Events.ItAcademy.Infrastructure.ArchivedEvents;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    await CreateHostBuilder(args).Build().RunAsync().ConfigureAwait(false);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed");
}
finally
{
    await Log.CloseAndFlushAsync();
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<ConnectionStrings>(hostContext.Configuration.GetSection(nameof(ConnectionStrings)));
        services.AddScoped<BaseRepository<Event>, EventRepository>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IEventSetupService, EventSetupService>();

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventSetupRepository, EventSetupRepository>();


        services.AddDbContext<EventsItAcademyContext>(options => options
        .UseSqlServer(hostContext.Configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection))));

        services.AddHostedService<Worker>();
        services.AddScoped<IArchivedEventService,ArchivedEventService>();
        services.AddScoped<IArchivedEventRepository,ArchivedEventRepository>();
    })
    .UseSerilog();