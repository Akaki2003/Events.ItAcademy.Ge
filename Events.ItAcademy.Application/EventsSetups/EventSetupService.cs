using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Application.Events.Responses;
using Events.ItAcademy.Application.EventsSetups.Repositories;
using Events.ItAcademy.Application.EventsSetups.Responses;
using Events.ItAcademy.Domain.EventSetup;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.EventsSetups
{
    public class EventSetupService : IEventSetupService
    {
        private readonly IEventSetupRepository _repository;

        public EventSetupService(IEventSetupRepository repository)
        {
            _repository = repository;
        }

        public async Task<EventSetupResponseModel> GetEventSetup(CancellationToken cancellation)
        {
            return (await _repository.GetEventSetupAsync(cancellation)).Adapt<EventSetupResponseModel>();
        }

        public async Task UpdateEventEditEventAfterUploadInDaysAsync(CancellationToken cancellationToken, int editEventAfterUploadInDays)
        {
            await _repository.UpdateEventEditEventAfterUploadInDaysAsync(cancellationToken, editEventAfterUploadInDays);
        }

        public async Task UpdateEventReserveTimeLengthAsync(CancellationToken cancellationToken, int reserveTimeLengthInMinutes)
        {
            await _repository.UpdateEventReserveTimeLengthAsync(cancellationToken, reserveTimeLengthInMinutes);
        }




    }
}
