using Events.ItAcademy.Domain.UserTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.UserTickets
{
    public interface IUserTicketService
    {
        Task BuyReservedTicket(CancellationToken cancellationToken, int ticketId, string userId);
        Task BuyStraightTicket(CancellationToken cancellationToken, int eventId, string userId);
        Task<List<UserTicket>> GetAllTicketsByUserId(CancellationToken cancellationToken, string userId);
        Task RemoveReservedTicket(CancellationToken cancellationToken, int ticketId, string userId);
        Task RemoveReservedTickets(CancellationToken cancellationToken);
        Task ReserveTicket(CancellationToken cancellationToken, int eventId, string userId);
    }
}
