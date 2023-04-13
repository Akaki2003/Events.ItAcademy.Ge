using Events.ItAcademy.Application.ArchivedEvents.Repositories;
using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Application.UserTickets.Repositories;
using Events.ItAcademy.Domain.Events;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.ArchivedEvents
{
    public class ArchivedEventService : IArchivedEventService
    {
        private readonly IArchivedEventRepository _repository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserTicketRepository _userTicketRepository;

        public ArchivedEventService(IArchivedEventRepository repository, IEventRepository eventRepository, IUserTicketRepository userTicketRepository)
        {
            _repository = repository;
            _eventRepository = eventRepository;
            _userTicketRepository = userTicketRepository;
        }

     
        public async Task AddToArchivedEvents(CancellationToken cancellationToken)
        {
            var eventsToArchive = await _eventRepository.GetAllExpiredEvents(cancellationToken);
            await _repository.AddToArchivedEvents(cancellationToken, eventsToArchive);

            foreach (var @event in eventsToArchive)
            {
                var tickets = await _userTicketRepository.GetAllTicketsByEventId(cancellationToken, @event.Id);
                await _userTicketRepository.RemoveTickets(cancellationToken, tickets);
                await _eventRepository.DeleteAsync(cancellationToken, @event.Id);
            }
        }
    }
}
