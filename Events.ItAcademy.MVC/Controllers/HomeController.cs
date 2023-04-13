using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.Events.Responses;
using Events.ItAcademy.MVC.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Events.ItAcademy.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventService _eventService;

        public HomeController(ILogger<HomeController> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var events = (await _eventService.GetAllAsyncActive(token)).Adapt<List<EventsWithoutDetails>>();
            return View(events);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}