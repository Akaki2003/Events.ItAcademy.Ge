using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Exceptions.Users
{
    public class UserNotFoundException : Exception
    {
        public readonly string Code = "UserNotFound";

        public UserNotFoundException() : base() { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception e) : base(message, e) { }
    }
}
