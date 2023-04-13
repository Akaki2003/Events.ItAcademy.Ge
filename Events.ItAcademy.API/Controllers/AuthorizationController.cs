using Events.ItAcademy.API.Infrastructure.Auth.JWT;
using Events.ItAcademy.Application.Users;
using Events.ItAcademy.Application.Users.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Events.ItAcademy.API.Controllers
{
    //[Route("v1/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<JWTConfiguration> _options;

        public AuthorizationController(IUserService userService, IOptions<JWTConfiguration> options)
        {
            _userService = userService;
            _options = options;
        }


        [Route("Register")]
        [HttpPost]
        public async Task Register(CancellationToken cancellation, UserCreateRequestModel user)
        {
            await _userService.CreateAsync(cancellation, user);
        }


        [Route("LogIn")]
        [HttpPost]
        public async Task<string> LogIn(CancellationToken cancellation, UserLoginRequestModel request)
        {
            var Id = await _userService.GetUserIdByEmail(cancellation, request.Email);
            var email = await _userService.AuthenticateAsync(cancellation, request.Email, request.Password);

            return JWTHelper.GenerateSecurityToken(email, Id, _options);
        }
    }
}
