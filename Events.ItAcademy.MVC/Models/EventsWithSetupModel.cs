using Events.ItAcademy.Application.Events.Responses;
using Events.ItAcademy.Application.EventsSetups.Responses;

namespace Events.ItAcademy.MVC.Models
{
    public class EventsWithSetupModel
    {
        public List<EventResponseModel> Events { get; set; }
        public EventSetupResponseModel EventSetup { get; set; }
    }
}
