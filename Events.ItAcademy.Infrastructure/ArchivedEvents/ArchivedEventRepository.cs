using Events.ItAcademy.Application.ArchivedEvents.Repositories;
using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Persistence.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Infrastructure.ArchivedEvents
{
    public class ArchivedEventRepository : BaseRepository<ArchivedEvent>, IArchivedEventRepository
    {
        public readonly IEventRepository _eventRepo;
        public ArchivedEventRepository(EventsItAcademyContext context, IEventRepository eventRepo) : base(context)
        {
            _eventRepo = eventRepo;
        }


        public async Task<List<ArchivedEvent>> GetAllArchivedEventsAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<ArchivedEvent>().ToListAsync(cancellationToken);
        }
        public async Task AddToArchivedEvents(CancellationToken cancellationToken, List<Event> events)
        {
            var archivedEvents = events.Adapt<List<ArchivedEvent>>();
           
            await _context.Set<ArchivedEvent>().AddRangeAsync(archivedEvents, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
