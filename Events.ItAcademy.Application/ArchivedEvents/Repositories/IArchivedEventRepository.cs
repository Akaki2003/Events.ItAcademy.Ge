using Events.ItAcademy.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.ArchivedEvents.Repositories
{
    public interface IArchivedEventRepository
    {
        Task AddToArchivedEvents(CancellationToken cancellationToken, List<Event> events);
    }
}
