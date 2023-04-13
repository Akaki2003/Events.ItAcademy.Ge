using Events.ItAcademy.Application.ArchivedEvents;
using Events.ItAcademy.Application.UserTickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.ItAcademy.API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("v1/[controller]")]
    //[Authorize]

    public class EventWorkerController : ControllerBase
    {
        private readonly IUserTicketService _ticketService;
        private readonly IArchivedEventService _archivedEventService;

        public EventWorkerController(IUserTicketService ticketService, IArchivedEventService archivedEventService)
        {
            _ticketService = ticketService;
            _archivedEventService = archivedEventService;
        }



        [HttpDelete("DeleteReservedTickets")]
        public async Task DeleteReservedTickets(CancellationToken cancellationToken)
        {
            await _ticketService.RemoveReservedTickets(cancellationToken);
        }

        [HttpDelete("ArchiveEvents")]
        public async Task ArchiveEvents(CancellationToken cancellationToken)
        {
            await _archivedEventService.AddToArchivedEvents(cancellationToken);
        }


    }
}
