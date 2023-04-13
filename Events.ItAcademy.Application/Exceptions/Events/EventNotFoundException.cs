namespace Events.ItAcademy.Application.Exceptions.Events
{
    public class EventNotFoundException : Exception
    {
        public readonly string Code = "EventNotFound";

        public EventNotFoundException() : base() { }
        public EventNotFoundException(string message) : base(message) { }
        public EventNotFoundException(string message, Exception e) : base(message, e) { }
    }
}
