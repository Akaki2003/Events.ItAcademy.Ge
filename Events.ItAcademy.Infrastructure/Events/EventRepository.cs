using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.Users;
using Events.ItAcademy.Persistence.Context;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Events.ItAcademy.Infrastructure.Events
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(EventsItAcademyContext context) : base(context)
        {

        }
        public async Task CreateAsync(CancellationToken cancellationToken, Event @event, string userId)
        {
            @event.ModifiedAt = DateTime.UtcNow;
            @event.CreatedAt = DateTime.UtcNow;
            @event.UserId = userId;  //service

            await AddAsync(cancellationToken, @event);
        }



        public async Task<bool> Exists(CancellationToken cancellationToken, int id)
        {
            return await AnyAsync(cancellationToken, x => x.Id == id);
        }

        public async Task<List<ArchivedEvent>> GetAllArchivedEventsAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<ArchivedEvent>().ToListAsync(cancellationToken);
        }
        public async Task AddToArchivedEvents(CancellationToken cancellationToken,List<Event> events)
        {
            var archivedEvents = events.Adapt<List<ArchivedEvent>>();
            await _context.Set<ArchivedEvent>().AddRangeAsync(archivedEvents, cancellationToken);
            _dbSet.RemoveRange(events);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<Event>> GetAllAsyncActive(CancellationToken cancellationToken) //normal users and moderators
        {
            return await _context.Set<Event>().Where(x => x.IsActive).ToListAsync();
        }
        

        public async Task<List<Event>> GetAllNonActiveAsync(CancellationToken cancellationToken) //for  admins
        {
            return await _context.Set<Event>().Where(x => !x.IsActive).ToListAsync();
        }
        public async Task<List<Event>> GetAllExpiredEvents(CancellationToken cancellationToken)
        {
            return await _context.Set<Event>().Where(x => DateTime.Now > x.EndDate).ToListAsync(cancellationToken);
        }

        public async Task<List<Event>> GetAllAsyncByUserId(CancellationToken cancellationToken, string userId) //normal users and moderators
        {
            return await _context.Set<Event>().Where(@event => @event.UserId == userId && @event.IsActive).ToListAsync();
        }
        public async Task<List<Event>> GetAllAsyncByUserIdNonActive(CancellationToken cancellationToken, string userId) //for admins
        {
            return await _context.Set<Event>().Where(@event => @event.UserId == userId && !@event.IsActive).ToListAsync(cancellationToken);
        }


        public async Task<DateTime> GetCreatedDate(CancellationToken cancellationToken, int id)
        {
            var @event = await _context.Set<Event>().SingleOrDefaultAsync(x => x.Id == id);
            var date = @event.CreatedAt;
            if (@event != null)
            {
                _context.Entry(@event).State = EntityState.Detached; //needs correction
            }
            _context.SaveChanges();

            return date;
        }

        public async Task<Event> GetEventByIdAsync(CancellationToken cancellationToken, int id)
        {
            return await _context.Set<Event>().SingleOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
        }
        public async Task<Event> GetNonActiveEvent(CancellationToken cancellationToken, int id)
        {
            return await _context.Set<Event>().SingleOrDefaultAsync(x => x.Id == id && !x.IsActive, cancellationToken);
        }

        public async Task UpdateEventAsync(CancellationToken cancellationToken, Event @event)
        {
            @event.ModifiedAt = DateTime.UtcNow;
            await UpdateAsync(cancellationToken, @event);
        }


        public async Task DeleteAsync(CancellationToken cancellationToken, int eventId)
        {
            var @event = await GetNonActiveEvent(cancellationToken, eventId);
            _context.Set<Event>().Remove(@event);

            await _context.SaveChangesAsync(cancellationToken) ;
        }

        public async Task RemoveRange(CancellationToken cancellationToken, List<Event> events)
        {
            _context.RemoveRange(events);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
