using Events.ItAcademy.Application.Roles.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

      
        public async Task InitializeUserRole(CancellationToken cancellationToken, string userId)
        {
            await _roleRepository.InitializeUserRole(cancellationToken, userId);
        }

        public async Task UpdateRole(CancellationToken token, string userId)
        {
            await _roleRepository.UpdateRole(token, userId);
        }
    }
}
