using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.Events.Requests;
using Events.ItAcademy.Application.Events.Responses;
using Events.ItAcademy.Application.UserTickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Events.ItAcademy.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IEventService _eventService;

        public EventsController(IHttpContextAccessor accessor, IEventService eventService)
        {
            _accessor = accessor;
            _eventService = eventService;
        }
        /// <summary>
        /// Get All Events
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetAllEvents")]
        [AllowAnonymous]
        public async Task<List<EventResponseModel>> GetAllEvents(CancellationToken cancellationToken)
        {
            return await _eventService.GetAllAsyncActive(cancellationToken);
        }

        /// <summary>
        /// Get Current User's Event With Id
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetCurrentUserEventWithId/{id}")]
        public async Task<EventResponseModel> GetEventById(CancellationToken cancellationToken, int id)
        {
            return await _eventService.GetByIdAsync(cancellationToken, id);
        }

        /// <summary>
        /// Get Current User Events With User Id
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetCurrentUserEventsWithUserId")]
        public async Task<List<EventResponseModel>> GetEventsByUserId(CancellationToken cancellationToken)
        {
            return await _eventService.GetAllAsyncByUserId(cancellationToken, GetUserId());
        }


        /// <summary>
        /// Create new event
        /// </summary>
        /// <param name="cancellationToken"></param>
        ///<param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(EventRequestModel), StatusCodes.Status200OK)]
        [HttpPost("CreateEvent")]
        public async Task Post(CancellationToken cancellationToken, [FromBody] EventRequestModel request)
        {
            await _eventService.CreateAsync(cancellationToken, request, GetUserId());
        }
        /// <summary>
        /// Update Event
        /// </summary>
        /// <param name="cancellationToken"></param>
        ///<param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(EventRequestPutModel), StatusCodes.Status200OK)]
        [HttpPut("UpdateEvent")]
        public async Task Put(CancellationToken cancellationToken, [FromBody] EventRequestPutModel request)
        {
            await _eventService.UpdateEventAsync(cancellationToken, request, GetUserId());
        }

        private string GetUserId()
        {
            var x = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            return x.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
