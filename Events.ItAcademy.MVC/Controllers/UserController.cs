using Events.ItAcademy.Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.ItAcademy.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllNormalUsers(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsers(cancellationToken);
            return View(users);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeModerator(CancellationToken cancellationToken,string userId)
        {
            await _userService.GiveModerator(cancellationToken, userId);
            return RedirectToAction(nameof(GetAllNormalUsers));
        }
    }
}
