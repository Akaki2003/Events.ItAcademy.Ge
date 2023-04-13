using Events.ItAcademy.Application.UserTickets;
using Events.ItAcademy.Application.UserTickets.Repositories;
using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.UserTickets;
using Events.ItAcademy.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Infrastructure.UserTickets
{
    public class UserTicketRepository : BaseRepository<UserTicket>, IUserTicketRepository
    {
        //private readonly EventsItAcademyContext _context;

        //public UserTicketRepository(EventsItAcademyContext context)
        //{
        //    _context = context;
        //}

        public UserTicketRepository(EventsItAcademyContext context) : base(context)
        {

        }

        public async Task<List<UserTicket>> GetAllTicketsByEventId(CancellationToken cancellationToken, int eventId)
        {
            return await _context.Set<UserTicket>().Where(x => x.EventId == eventId).ToListAsync();
        }

        public async Task<List<UserTicket>> GetAllTicketsByUserId(CancellationToken cancellationToken, string userId)
        {
            return await _context.Set<UserTicket>().Where(x => x.UserId == userId).Include(x=>x.Event).ToListAsync();
        }


        public async Task<UserTicket> GetTicketByTicketAndUserId(CancellationToken cancellationToken, int ticketId, string userId)
        {
            return await _context.Set<UserTicket>().SingleOrDefaultAsync(x => x.TicketId == ticketId && x.UserId==userId, cancellationToken);
        }
        public async Task RemoveReservedTicket(CancellationToken cancellationToken, UserTicket ticket)
        {
            _context.Remove(ticket);
            await _context.SaveChangesAsync(cancellationToken);
        }


        //all
        public async Task RemoveExpiredTickets(CancellationToken cancellationToken,List<UserTicket> tickets)
        {
             _context.RemoveRange( tickets);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<UserTicket>> GetExpiredTickets(CancellationToken cancellationToken,double waitingTime)
        {
            return await _context.Set<UserTicket>().Where(x => x.Reserved && 
            (x.ReservationDate.Value.AddMinutes(waitingTime) < DateTime.Now 
            || DateTime.Now > x.Event.StartDate))
            .ToListAsync(cancellationToken);
        }
        public async Task RemoveTickets(CancellationToken cancellationToken, List<UserTicket> tickets)
        {
            _context.RemoveRange(tickets);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateTicket(CancellationToken cancellationToken,UserTicket ticket)
        {
            _context.Update(ticket);
            await _context.SaveChangesAsync(cancellationToken);
        }



        public async Task BuyTicket(CancellationToken cancellationToken,UserTicket ticket)
        {
            await _context.Set<UserTicket>().AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCurrentTicketCountByEventId(CancellationToken cancellation,int eventId)
        {
            return await _context.Set<UserTicket>().Where(x => x.EventId == eventId).CountAsync(cancellation);
        }
    }
}
