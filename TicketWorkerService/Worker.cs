using Events.ItAcademy.Application.UserTickets;
using Events.ItAcademy.Application.UserTickets.Repositories;

namespace TicketWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromSeconds(3));

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker Started at: {time}", DateTimeOffset.Now);
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<IUserTicketService>();

                        await service.RemoveReservedTickets(stoppingToken);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }
        }
    }
}