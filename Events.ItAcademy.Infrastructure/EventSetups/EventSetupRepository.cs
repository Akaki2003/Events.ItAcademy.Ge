using Events.ItAcademy.Application.EventsSetups.Repositories;
using Events.ItAcademy.Application.EventsSetups.Responses;
using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.EventSetup;
using Events.ItAcademy.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Infrastructure.EventSetups
{
    public class EventSetupRepository : BaseRepository<EventSetup>, IEventSetupRepository
    {
        public EventSetupRepository(EventsItAcademyContext context) : base(context)
        {

        }

        public async Task<EventSetup> GetEventSetupAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<EventSetup>().SingleAsync(cancellationToken);
        }

        public async Task UpdateEventReserveTimeLengthAsync(CancellationToken cancellationToken, int reserveTimeLengthInMinutes)
        {
            var eventSetup = await GetEventSetupAsync(cancellationToken);
            eventSetup.ReserveTimeLengthInMinutes = reserveTimeLengthInMinutes;
            await UpdateAsync(cancellationToken, eventSetup);
        }

        public async Task UpdateEventEditEventAfterUploadInDaysAsync(CancellationToken cancellationToken, int editEventAfterUploadInDays)
        {
            var eventSetup = await GetEventSetupAsync(cancellationToken);
            eventSetup.EditEventAfterUploadInDays = editEventAfterUploadInDays;
            await UpdateAsync(cancellationToken, eventSetup);
        }


    }
}
