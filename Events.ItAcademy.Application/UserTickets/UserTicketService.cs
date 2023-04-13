using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Application.EventsSetups.Repositories;
using Events.ItAcademy.Application.Exceptions.Events;
using Events.ItAcademy.Application.Exceptions.Tickets;
using Events.ItAcademy.Application.Localization;
using Events.ItAcademy.Application.UserTickets.Repositories;
using Events.ItAcademy.Domain.UserTickets;

namespace Events.ItAcademy.Application.UserTickets
{
    public class UserTicketService : IUserTicketService
    {
        private readonly IUserTicketRepository _repo;
        private readonly IEventRepository _eventRepo;
        private readonly IEventSetupRepository _eventSetupRepository;

        public UserTicketService(IUserTicketRepository repo, IEventRepository eventRepo, IEventSetupRepository eventSetupRepository)
        {
            _repo = repo;
            _eventRepo = eventRepo;
            _eventSetupRepository = eventSetupRepository;
        }


       

        public async Task<List<UserTicket>> GetAllTicketsByUserId(CancellationToken cancellationToken, string userId)
        {
            return await _repo.GetAllTicketsByUserId(cancellationToken, userId);
        }


        


        public async Task BuyReservedTicket(CancellationToken cancellationToken,int ticketId, string userId)
        {
            var ticket = await _repo.GetTicketByTicketAndUserId(cancellationToken, ticketId,userId);
            if(ticket == null)
            {
                throw new TicketNotFoundException(ErrorMessages.TicketNotFound);
            }
            if (ticket.Reserved == false)
            {
                throw new TicketAlreadyPurchasedException(ErrorMessages.TicketAlreadyPurchased);
            }
            ticket.Reserved = false;
            await _repo.UpdateTicket(cancellationToken,ticket);
        }

        public async Task ReserveTicket(CancellationToken cancellationToken, int eventId, string userId)
        {
            var @event = await _eventRepo.GetEventByIdAsync(cancellationToken, eventId);
            if (@event == null)
            {
                throw new EventNotFoundException(ErrorMessages.EventNotFound);
            }
            var availableTickets = @event.TicketCount - await _repo.GetCurrentTicketCountByEventId(cancellationToken, eventId);
            if (availableTickets == 0)
            {
                throw new TicketNotFoundException(ErrorMessages.TicketNotFound);
            }
            UserTicket ticket = new UserTicket()
            {
                UserId = userId,
                EventId = eventId,
                Reserved = true,
                ReservationDate = DateTime.Now
            };

            await _repo.BuyTicket(cancellationToken, ticket);
        }

        public async Task BuyStraightTicket(CancellationToken cancellationToken, int eventId, string userId)
        {
            var @event = await _eventRepo.GetEventByIdAsync(cancellationToken, eventId);
            if (@event == null)
            {
                throw new EventNotFoundException(ErrorMessages.EventNotFound);
            }
            var availableTickets = @event.TicketCount - await _repo.GetCurrentTicketCountByEventId(cancellationToken, eventId);
            if (availableTickets <= 0)
            {
                throw new TicketNotFoundException(ErrorMessages.TicketNotFound);
            }
            UserTicket ticket = new UserTicket()
            {
                UserId = userId,
                EventId = eventId,
                Reserved = false,
            };

            await _repo.BuyTicket(cancellationToken, ticket);
        }


        public async Task RemoveReservedTicket(CancellationToken cancellationToken, int ticketId, string userId)
        {
            var ticket = await _repo.GetTicketByTicketAndUserId(cancellationToken, ticketId, userId);
            if (ticket == null)
            {
                throw new TicketNotFoundException(ErrorMessages.TicketNotFound);
            }
            await _repo.RemoveReservedTicket(cancellationToken, ticket);
        }

        public async Task RemoveReservedTickets(CancellationToken cancellationToken)
        {
            var eventSetup = await _eventSetupRepository.GetEventSetupAsync(cancellationToken);
            var expiredTickets = await _repo.GetExpiredTickets(cancellationToken, eventSetup.ReserveTimeLengthInMinutes);
            await _repo.RemoveExpiredTickets(cancellationToken, expiredTickets);
        }

    }
}
