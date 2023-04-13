using Events.ItAcademy.Application.Users.Repositories;
using Events.ItAcademy.Domain.Users;
using Events.ItAcademy.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {


        public UserRepository(EventsItAcademyContext context) : base(context)
        {

        }


        public async Task<string> CreateAsync(CancellationToken cancellationToken, User user)
        {
            user.CreatedAt = DateTime.Now;
            user.ModifiedAt = DateTime.Now;
            await _dbSet.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, string username)
        {
            return await base.AnyAsync(cancellationToken, x => x.UserName == username);
        }


        public async Task<bool> ExistsByEmail(CancellationToken cancellationToken, string email)
        {
            return await base.AnyAsync(cancellationToken, x => x.Email == email);
        }

        public async Task<List<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<User> GetByEmailAndPassword(CancellationToken cancellationToken, string email, string password)
        {
            return await _context.Set<User>().SingleOrDefaultAsync(x => x.Email == email && password == x.PasswordHash, cancellationToken);
        }

        public async Task<string> GetUserIdByEmail(CancellationToken cancellationToken, string username)
        {
            var user = await _context.Set<User>().SingleOrDefaultAsync(x => x.UserName == username);
            return user.Id;
        }
       

   
        public User GetUser(string id)
        {
            return _context.Set<User>().FirstOrDefault(u => u.Id == id);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Set<User>().ToList();
        }

        public User UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
