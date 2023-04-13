using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Application.Events.Requests;
using Events.ItAcademy.Application.Events.Responses;
using Events.ItAcademy.Application.EventsSetups.Repositories;
using Events.ItAcademy.Application.Exceptions;
using Events.ItAcademy.Application.Exceptions.Events;
using Events.ItAcademy.Application.Localization;
using Events.ItAcademy.Application.Roles;
using Events.ItAcademy.Application.UserTickets.Repositories;
using Events.ItAcademy.Domain.Events;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;

namespace Events.ItAcademy.Application.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;
        private readonly IUserTicketRepository _ticketRepo;
        private readonly IEventSetupRepository _eventSetupRepository;

        public EventService(IEventRepository repository, IEventSetupRepository eventSetupRepository, IUserTicketRepository ticketRepo)
        {
            _repository = repository;
            _eventSetupRepository = eventSetupRepository;
            _ticketRepo = ticketRepo;
        }
        public async Task<List<EventResponseModel>> GetAllAsyncActive(CancellationToken cancellationToken)
        {
            return (await _repository.GetAllAsyncActive(cancellationToken)).Adapt<List<EventResponseModel>>();
        }

        public async Task<List<EventResponseModel>> GetAllNonActiveAsync(CancellationToken cancellationToken) //for  admins
        {
            return (await _repository.GetAllNonActiveAsync(cancellationToken)).Where(x=>!x.IsActive)
                .ToList().Adapt<List<EventResponseModel>>();
        }

        public async Task<List<EventResponseModel>> GetAllAsyncByUserId(CancellationToken cancellationToken, string userId) //normal users and moderators
        {
            var events = await _repository.GetAllAsyncByUserId(cancellationToken, userId);
            return events.Adapt<List<EventResponseModel>>();
        }
        public async Task<List<EventResponseModel>> GetAllAsyncByUserIdNonActive(CancellationToken cancellationToken, string userId) //for admins
        {
            var events = await _repository.GetAllAsyncByUserIdNonActive(cancellationToken, userId);
            return events.Adapt<List<EventResponseModel>>();
        }

        public async Task CreateAsync(CancellationToken cancellationToken, EventRequestModel @event, string userId)
        {
            if(@event == null || userId == null)
            {
                throw new ArgumentNullException();
            }
            var eventToInsert= @event.Adapt<Event>();
            await _repository.CreateAsync(cancellationToken, eventToInsert, userId);
        }

       
        
        public async Task<EventResponseModel> GetByIdAsync(CancellationToken cancellationToken, int eventId)
        {
            if (!await _repository.Exists(cancellationToken, eventId))
                throw new EventNotFoundException(ErrorMessages.EventNotFound); 
            var @event = await _repository.GetEventByIdAsync(cancellationToken, eventId);
            var ticketCount = await _ticketRepo.GetCurrentTicketCountByEventId(cancellationToken, eventId);
            @event.TicketCount -= ticketCount;
            return @event.Adapt<EventResponseModel>();
        }

        public async Task UpdateEventAsync(CancellationToken cancellationToken, EventRequestPutModel @event, string userId)
        {
            var originalEvent = await _repository.GetEventByIdAsync(cancellationToken, @event.Id);
            if(originalEvent is null)
                throw new EventNotFoundException(ErrorMessages.EventNotFound);

            var eventSetup = await _eventSetupRepository.GetEventSetupAsync(cancellationToken);
            if (originalEvent.CreatedAt+TimeSpan.FromDays(eventSetup.EditEventAfterUploadInDays)<DateTime.Now)
            {
                throw new EventEditExpiredException(ErrorMessages.EventEditTimeExpired);
            }
            var eventToUpdate = @event.Adapt<Event>();
            eventToUpdate.StartDate = originalEvent.StartDate;
            eventToUpdate.EndDate = originalEvent.EndDate;
            eventToUpdate.IsActive = originalEvent.IsActive;

            var created = await _repository.GetCreatedDate(cancellationToken, @event.Id);
            eventToUpdate.CreatedAt = created;
            eventToUpdate.UserId = userId;
            await _repository.UpdateEventAsync(cancellationToken, eventToUpdate);
        }


        public async Task DeleteAsync(CancellationToken cancellationToken, int id, string userId)
        {
            if (!await _repository.Exists(cancellationToken, id))
                throw new EventNotFoundException(ErrorMessages.EventNotFound);
            await _repository.DeleteAsync(cancellationToken, id);
        }

        public async Task ActivateEvent(CancellationToken cancellationToken,int eventId)
        {
            if (!await _repository.Exists(cancellationToken, eventId))
                throw new EventNotFoundException(ErrorMessages.EventNotFound);
            var @event = await _repository.GetNonActiveEvent(cancellationToken, eventId);
            @event.IsActive = true;
            await _repository.UpdateEventAsync(cancellationToken, @event);
        }
    }
}
