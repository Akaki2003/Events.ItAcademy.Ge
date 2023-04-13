using Events.ItAcademy.Domain.Events;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Events.Repositories
{
    public interface IEventRepository
    {
        Task CreateAsync(CancellationToken cancellationToken, Event @event, string userId);
        Task DeleteAsync(CancellationToken cancellationToken, int eventId);
        Task<bool> Exists(CancellationToken cancellationToken, int id);
        Task<List<Event>> GetAllAsyncActive(CancellationToken cancellationToken);
        Task<List<Event>> GetAllAsyncByUserId(CancellationToken cancellationToken, string userId);
        Task<List<Event>> GetAllAsyncByUserIdNonActive(CancellationToken cancellationToken, string userId);
        Task<List<Event>> GetAllExpiredEvents(CancellationToken cancellationToken);
        Task<List<Event>> GetAllNonActiveAsync(CancellationToken cancellationToken);
        Task<DateTime> GetCreatedDate(CancellationToken cancellationToken, int id);
        Task<Event> GetEventByIdAsync(CancellationToken cancellationToken, int id);
        Task<Event> GetNonActiveEvent(CancellationToken cancellationToken, int id);
        Task RemoveRange(CancellationToken cancellationToken, List<Event> events);
        Task UpdateEventAsync(CancellationToken cancellationToken, Event @event);
    }
}
