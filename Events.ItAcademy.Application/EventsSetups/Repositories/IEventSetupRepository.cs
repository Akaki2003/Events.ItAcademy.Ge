using Events.ItAcademy.Domain.EventSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.EventsSetups.Repositories
{
    public interface IEventSetupRepository
    {
        Task<EventSetup> GetEventSetupAsync(CancellationToken cancellationToken);
        Task UpdateEventEditEventAfterUploadInDaysAsync(CancellationToken cancellationToken, int editEventAfterUploadInDays);
        Task UpdateEventReserveTimeLengthAsync(CancellationToken cancellationToken, int reserveTimeLengthInMinutes);
    }
}
