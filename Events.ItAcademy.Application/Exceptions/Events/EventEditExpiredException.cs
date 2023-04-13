using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Exceptions.Events
{
    public class EventEditExpiredException : Exception
    {
        public readonly string Code = "Event edit time has expired";

        public EventEditExpiredException() : base() { }
        public EventEditExpiredException(string message) : base(message) { }
        public EventEditExpiredException(string message, Exception e) : base(message, e) { }
    }
}
