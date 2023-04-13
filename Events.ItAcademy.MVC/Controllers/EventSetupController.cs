using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.EventsSetups;
using Events.ItAcademy.Application.EventsSetups.Responses;
using Events.ItAcademy.Domain.EventSetup;
using Events.ItAcademy.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using System.Threading;

namespace Events.ItAcademy.MVC.Controllers
{
    public class EventSetupController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IEventSetupService _eventSetupService;

        public EventSetupController(IHttpContextAccessor accessor, IEventSetupService eventSetupService)
        {
            _accessor = accessor;
            _eventSetupService = eventSetupService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEventSetup(CancellationToken cancellationToken)
        {
            var eventSetup =  await _eventSetupService.GetEventSetup(cancellationToken);

            return View(eventSetup);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEventReserve(CancellationToken cancellationToken,int reserveTimeLengthInMinutes)
        {
            if (reserveTimeLengthInMinutes < 1)
            {
                return RedirectToAction(nameof(GetEventSetup));
            }
            await _eventSetupService.UpdateEventReserveTimeLengthAsync(cancellationToken, reserveTimeLengthInMinutes);

            return RedirectToAction(nameof(GetEventSetup));
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEventEditTime(CancellationToken cancellationToken, int editEventAfterUploadInDays)
        {
            if (editEventAfterUploadInDays < 1)
            {
                return RedirectToAction(nameof(GetEventSetup));
            }
            await _eventSetupService.UpdateEventEditEventAfterUploadInDaysAsync(cancellationToken, editEventAfterUploadInDays);

            return RedirectToAction(nameof(GetEventSetup));
        }


        private string GetUserId()
        {
            var x = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            return x.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
