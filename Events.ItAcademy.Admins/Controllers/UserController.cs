using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Events.ItAcademy.Application.UnitOfWork;
using Events.ItAcademy.Domain.Users;
using Events.ItAcademy.Application.Users.Requests;

namespace Events.ItAcademy.Admins.Controllers
{
    [Authorize(Policy ="AdminOnly")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();
            return View(users);
        }

        //public async Task<IActionResult> Edit(string id)
        //{
        //    var user = _unitOfWork.User.GetUser(id);
        //    var roles = await _unitOfWork.Role.GetUserRoles(id);

        //    var userRoles = await _userManager.GetRolesAsync(user);

        //    var roleItems = roles.Select(role =>
        //        new SelectListItem(
        //            role.Name,
        //            role.Id,
        //            userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

        //    var vm = new EditUserViewModel
        //    {
        //        User = user,
        //        Roles = roleItems
        //    };

        //    return View(vm);
        //}

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _userManager.GetRolesAsync(user);

            //Loop through the roles in ViewModel
            //Check if the Role is Assigned In DB
            //If Assigned -> Do Nothing
            //If Not Assigned -> Add Role

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();

            foreach (var role in data.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _userManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToDelete.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            user.UserName = data.User.UserName;
            user.NormalizedUserName = data.User.NormalizedUserName;
            user.NormalizedEmail = data.User.NormalizedEmail;
            user.Email = data.User.Email;

             _unitOfWork.User.UpdateUser(user);

            return RedirectToAction("Edit", new { id = user.Id });
        }
    }
}
