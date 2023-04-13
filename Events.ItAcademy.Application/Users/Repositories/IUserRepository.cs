using Events.ItAcademy.Domain.Users;

namespace Events.ItAcademy.Application.Users.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> GetByEmailAndPassword(CancellationToken cancellationToken, string email, string password);
        Task <string> CreateAsync(CancellationToken cancellationToken, User user);
        Task UpdateAsync(CancellationToken cancellationToken, User user);
        Task<bool> Exists(CancellationToken cancellationToken, string username);
        Task<bool> ExistsByEmail(CancellationToken cancellationToken, string email);
        Task<string> GetUserIdByEmail(CancellationToken cancellationToken, string name);
        Task<List<User>> GetAllUsers(CancellationToken cancellationToken);

        ICollection<User> GetUsers();

        User GetUser(string id);

        User UpdateUser(User user);
    }
}
