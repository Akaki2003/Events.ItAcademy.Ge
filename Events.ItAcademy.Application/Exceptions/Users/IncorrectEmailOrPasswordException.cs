using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Exceptions.Users
{
    public class IncorrectEmailOrPasswordException : Exception
    {
        public readonly string Code = "IncorrectEmailOrPassword";

        public IncorrectEmailOrPasswordException() : base() { }
        public IncorrectEmailOrPasswordException(string message) : base(message) { }
        public IncorrectEmailOrPasswordException(string message, Exception e) : base(message, e) { }
    }
}
