using Events.ItAcademy.Application.Exceptions.Events;
using Events.ItAcademy.Application.Exceptions.Tickets;
using Events.ItAcademy.Application.Exceptions.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Events.ItAcademy.Application.ErrorHandler
{
    public class ApiError : ProblemDetails
    {
        public const string UnhandlerErrorCode = "UnhandledError";
        private HttpContext _httpContext;
        private Exception _exception;



        public LogLevel LogLevel { get; set; }
        public string Code { get; set; }

        public string TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string)traceId;
                }

                return null;
            }

            set => Extensions["TraceId"] = value;
        }

        public ApiError(HttpContext httpContext, Exception exception)
        {
            _httpContext = httpContext;
            _exception = exception;

            TraceId = httpContext.TraceIdentifier;

            //default
            Code = UnhandlerErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Title = exception.Message;
            LogLevel = LogLevel.Error;
            Instance = httpContext.Request.Path;

            HandleException((dynamic)exception);
        }


        private void HandleException(EventNotFoundException exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = exception.Source;
            LogLevel = LogLevel.Information;
        }
        private void HandleException(EventEditExpiredException exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.MethodNotAllowed;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.5";
            Title = exception.Source;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(TicketNotFoundException exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = exception.Source;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(UserNotFoundException exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = exception.Source;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(UserAlreadyExistsException exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.Conflict;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
            Title = exception.Source;
            LogLevel = LogLevel.Information;
        }
        private void HandleException(IncorrectEmailOrPasswordException exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.Unauthorized;
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
            Title = exception.Source;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(Exception exception)
        {
        }
    }
}
