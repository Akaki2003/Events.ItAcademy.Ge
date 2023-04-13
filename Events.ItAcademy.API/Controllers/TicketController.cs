using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.UserTickets;
using Events.ItAcademy.Domain.UserTickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Events.ItAcademy.API.Controllers
{
    [ApiController]
    //[Route("v1/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]


    public class TicketController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserTicketService _ticketService;

        public TicketController(IHttpContextAccessor accessor, IUserTicketService ticketService)
        {
            _accessor = accessor;
            _ticketService = ticketService;
        }
        [HttpGet]
        public async Task<List<UserTicket>> MyTickets(CancellationToken cancellationToken)
        {
            return await _ticketService.GetAllTicketsByUserId(cancellationToken, GetUserId());
        }

        [HttpPost("BookTicket")]
        public async Task BookTicket(CancellationToken cancellationToken,int eventId)
        {
            await _ticketService.ReserveTicket(cancellationToken,eventId, GetUserId());
        }

        [HttpPost("BuyReservedTicket")]
        public async Task BuyReservedTicket(CancellationToken cancellationToken, int ticketId)
        {
            await _ticketService.BuyReservedTicket(cancellationToken, ticketId, GetUserId());
        }  
        
        [HttpPost("BuyTicketStraight")]
        public async Task BuyStraightTicket(CancellationToken cancellationToken, int eventId)
        {
            await _ticketService.BuyStraightTicket(cancellationToken, eventId, GetUserId());
        }


        [HttpDelete("DeleteReserved")]
        public async Task DeleteReserved(CancellationToken cancellationToken,int ticketId)
        {
            await _ticketService.RemoveReservedTicket(cancellationToken, ticketId, GetUserId());
        }

        private string GetUserId()
        {
            var x = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            return x.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
