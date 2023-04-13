using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Exceptions.Users
{
    public class UserAlreadyExistsException : Exception
    {
        public readonly string Code = "UserAlreadyExists";

        public UserAlreadyExistsException() : base() { }
        public UserAlreadyExistsException(string message) : base(message) { }
        public UserAlreadyExistsException(string message, Exception e) : base(message, e) { }
    }
}
