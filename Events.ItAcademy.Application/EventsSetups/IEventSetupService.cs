using Events.ItAcademy.Application.Events.Responses;
using Events.ItAcademy.Application.EventsSetups.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.EventsSetups
{
    public interface IEventSetupService
    {
        Task<EventSetupResponseModel> GetEventSetup(CancellationToken cancellation);
        Task UpdateEventEditEventAfterUploadInDaysAsync(CancellationToken cancellationToken, int editEventAfterUploadInDays);
        Task UpdateEventReserveTimeLengthAsync(CancellationToken cancellationToken, int reserveTimeLengthInMinutes);
    }
}
