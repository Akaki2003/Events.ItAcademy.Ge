using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.ItAcademy.Application.Roles.Repositories;
using Events.ItAcademy.Domain.Users;
using Events.ItAcademy.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Events.ItAcademy.Infrastructure.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EventsItAcademyContext _context;

        public RoleRepository(EventsItAcademyContext context)
        {
            _context = context;
        }

        public async Task InitializeUserRole(CancellationToken cancellationToken, string userId)
        {
            var roleId = await GetUserRole();
            IdentityUserRole<string> userWithRole = new IdentityUserRole<string>();
            userWithRole.RoleId = roleId;
            userWithRole.UserId = userId;
            await _context.UserRoles.AddAsync(userWithRole, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> GetUserRole()
        {
            var role = await _context.Roles
                .SingleOrDefaultAsync(usr => usr.Name == "User");

            return role.Id;
        }
        public async Task UpdateRole(CancellationToken token, string userId)
        {
            var roleId = "2";
            var role = await _context.UserRoles.Where(x => x.UserId == userId).FirstOrDefaultAsync(token);
            _context.UserRoles.Remove(role);
            _context.SaveChanges();
            role.RoleId = roleId;
            _context.UserRoles.Add(role);
            //_context.Update(role);
            _context.SaveChanges();

        }

        public async Task<List<string>> GetNormalUserIdsByRole(CancellationToken cancellationToken)
        {
            return (await _context.UserRoles.Where(x => x.RoleId == 3.ToString()).Select(x => x.UserId).ToListAsync(cancellationToken));
        }
    }

}