using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Roles.Repositories
{
    public interface IRoleRepository
    {
        Task UpdateRole(CancellationToken token, string userId);
        Task<string> GetUserRole();
        Task InitializeUserRole(CancellationToken cancellationToken, string userId);
        Task<List<string>> GetNormalUserIdsByRole(CancellationToken cancellationToken);
    }
}
