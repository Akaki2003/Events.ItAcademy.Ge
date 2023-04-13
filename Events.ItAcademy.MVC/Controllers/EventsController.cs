using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.Events.Requests;
using Events.ItAcademy.Application.Events.Responses;
using Events.ItAcademy.Application.EventsSetups;
using Events.ItAcademy.MVC.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Security.Claims;

namespace Events.ItAcademy.MVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEventSetupService _eventSetup;
        private readonly IWebHostEnvironment hostingEnvironment;

        public EventsController(IEventService eventService, IHttpContextAccessor accessor, IEventSetupService eventSetup, IWebHostEnvironment hostingEnvironment)
        {
            _eventService = eventService;
            _accessor = accessor;
            _eventSetup = eventSetup;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> NonActiveEvents(CancellationToken token)
        {
            var events = await _eventService.GetAllNonActiveAsync(token);
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> ViewDetails(int eventId, CancellationToken token = default(CancellationToken))
        {
            var @event = await _eventService.GetByIdAsync(token, eventId);
            return View(@event);
        }
        [Authorize]
        public IActionResult AddEvent()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddEvent(CancellationToken token, EventCreateViewModel @event)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                string filePath = null;
                if(@event.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + @event.Photo.FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    await @event.Photo.CopyToAsync(new FileStream( filePath,FileMode.Create), token);
                }
                //add photopath to event
                var eventToCreate = @event.Adapt<EventRequestModel>();
                eventToCreate.PhotoPath = uniqueFileName;
                await _eventService.CreateAsync(token, eventToCreate, GetUserId());
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("AddEvent", "Events");
        }


        [Authorize(Roles = "Admin")]
        //[HttpPut]
        public async Task<IActionResult> ActivateEvent(CancellationToken token, int eventId)
        {
            await _eventService.ActivateEvent(token, eventId);
            return RedirectToAction(nameof(NonActiveEvents));
        }
        public async Task<IActionResult> MyEvents(CancellationToken token)
        {
            var events = await _eventService.GetAllAsyncByUserId(token, GetUserId());
            var EventSetup = await _eventSetup.GetEventSetup(token);
            var result = new EventsWithSetupModel()
            {
                EventSetup = EventSetup,
                Events = events
            };
            return View(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditEvent(CancellationToken token, int eventId)
        {
            var @event = await _eventService.GetByIdAsync(token, eventId);
            return View(@event);
        }


        [Authorize]
        public async Task<IActionResult> EditEvent(CancellationToken token, EventRequestPutModel @event)
        {
            if (ModelState.IsValid)
            {
                await _eventService.UpdateEventAsync(token, @event, GetUserId());
                return RedirectToAction(nameof(MyEvents));
            }
            return RedirectToAction("EditEvent", "Events", new { eventId = @event.Id });
        }




        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> DeleteEvent(CancellationToken token, int eventId)
        {
            await _eventService.DeleteAsync(token, eventId, GetUserId());
            return RedirectToAction(nameof(NonActiveEvents));
        }


        [Authorize(Roles = "Admin")]
        public IActionResult EventSettings()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public IActionResult EventSettings(CancellationToken token, EditSettingsModel editSettings)
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetUserId()
        {
            var x = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            return x.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}