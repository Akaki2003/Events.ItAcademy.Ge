using Events.ItAcademy.Domain.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Events.ItAcademy.Application.Users.Requests
{
    public class EditUserViewModel
    {
        public User User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}
