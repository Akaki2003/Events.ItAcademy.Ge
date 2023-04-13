using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Roles
{
    public interface IRoleService
    {
        Task InitializeUserRole(CancellationToken cancellationToken, string userId);
        Task UpdateRole(CancellationToken token, string userId);
    }
}
