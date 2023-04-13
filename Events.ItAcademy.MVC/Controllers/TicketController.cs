using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.UserTickets;
using Events.ItAcademy.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Events.ItAcademy.MVC.Controllers
{
    public class TicketController : Controller
    {
        private readonly IUserTicketService _ticketService;

        private readonly IHttpContextAccessor _accessor;

        public TicketController(IUserTicketService ticketService, IHttpContextAccessor accessor)
        {
            _ticketService = ticketService;
            _accessor = accessor;
        }


        [Authorize]
        public async Task<IActionResult> BookTicket(CancellationToken cancellationToken, int eventId)
        {
            await _ticketService.ReserveTicket(cancellationToken, eventId, GetUserId());
            return RedirectToAction("ViewDetails", "Events", new {eventId = eventId});
        }

        [Authorize]
        public async Task<IActionResult> BuyBooked(CancellationToken cancellationToken, int ticketId)
        {
            await _ticketService.BuyReservedTicket(cancellationToken, ticketId, GetUserId());
            return RedirectToAction("MyTickets", "Ticket", ticketId);
        }

        [Authorize]
        public async Task<IActionResult> BuyStraightTicket(CancellationToken cancellationToken, int eventId)
        {
            await _ticketService.BuyStraightTicket(cancellationToken, eventId, GetUserId());
            return RedirectToAction("ViewDetails","Events", new { eventId = eventId });
        }


        [Authorize]
        public async Task<IActionResult> MyTickets(CancellationToken cancellationToken)
        {
            var tickets = await _ticketService.GetAllTicketsByUserId(cancellationToken, GetUserId());
            return View(tickets);
        }

        [Authorize]
        public async Task<IActionResult> RemoveReservedTicket(CancellationToken cancellationToken, int ticketId)
        {
            await _ticketService.RemoveReservedTicket(cancellationToken, ticketId, GetUserId());
            return RedirectToAction("MyTickets", "Ticket");
        }

        private string GetUserId()
        {
            var x = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            return x.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
