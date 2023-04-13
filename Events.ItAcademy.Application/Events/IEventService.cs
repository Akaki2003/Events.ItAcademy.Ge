using Events.ItAcademy.Application.Events.Requests;
using Events.ItAcademy.Application.Events.Responses;
using Events.ItAcademy.Domain.Events;
using Microsoft.AspNetCore.JsonPatch;

namespace Events.ItAcademy.Application.Events
{
    public interface IEventService
    {
        Task ActivateEvent(CancellationToken cancellationToken, int eventId);
        Task CreateAsync(CancellationToken cancellationToken, EventRequestModel @event, string userId);
        Task DeleteAsync(CancellationToken cancellationToken, int id, string userId);
        Task<List<EventResponseModel>> GetAllAsyncActive(CancellationToken cancellationToken);
        Task<List<EventResponseModel>> GetAllAsyncByUserId(CancellationToken cancellationToken, string userId);
        Task<List<EventResponseModel>> GetAllAsyncByUserIdNonActive(CancellationToken cancellationToken, string userId);
        Task<List<EventResponseModel>> GetAllNonActiveAsync(CancellationToken cancellationToken/*, string UserId*/);
        Task<EventResponseModel> GetByIdAsync(CancellationToken cancellationToken, int eventId);
        Task UpdateEventAsync(CancellationToken cancellationToken, EventRequestPutModel @event, string userId);
    }
}
