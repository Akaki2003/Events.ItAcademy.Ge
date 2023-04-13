using Events.ItAcademy.Domain.UserTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.UserTickets.Repositories
{
    public interface IUserTicketRepository
    {
        Task BuyTicket(CancellationToken cancellationToken, UserTicket ticket);
        Task<List<UserTicket>> GetAllTicketsByEventId(CancellationToken cancellationToken, int eventId);
        Task<List<UserTicket>> GetAllTicketsByUserId(CancellationToken cancellationToken, string userId);
        Task<int> GetCurrentTicketCountByEventId(CancellationToken cancellation, int eventId);
        Task<List<UserTicket>> GetExpiredTickets(CancellationToken cancellationToken, double waitingTime);
        Task<UserTicket> GetTicketByTicketAndUserId(CancellationToken cancellationToken, int ticketId, string userId);
        Task RemoveExpiredTickets(CancellationToken cancellationToken, List<UserTicket> tickets);
        Task RemoveReservedTicket(CancellationToken cancellationToken, UserTicket ticket);
        Task RemoveTickets(CancellationToken cancellationToken, List<UserTicket> tickets);
        Task UpdateTicket(CancellationToken cancellationToken, UserTicket ticket);
    }
}
