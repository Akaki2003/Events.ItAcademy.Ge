using Events.ItAcademy.Application.Users.Requests;
using Events.ItAcademy.Application.Users.Responses;
using Events.ItAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Users
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(CancellationToken cancellation, string email, string password);
        Task CreateAsync(CancellationToken cancellation, UserCreateRequestModel user);
        Task<List<UserResponseModel>> GetAllUsers(CancellationToken cancellationToken);
        Task<string> GetUserIdByEmail(CancellationToken cancellationToken, string Name);
        Task GiveModerator(CancellationToken token, string userId);
    }
}
