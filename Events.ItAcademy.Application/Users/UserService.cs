using Events.ItAcademy.Application.Exceptions;
using Events.ItAcademy.Application.Exceptions.Users;
using Events.ItAcademy.Application.Localization;
using Events.ItAcademy.Application.Roles;
using Events.ItAcademy.Application.Roles.Repositories;
using Events.ItAcademy.Application.Users.Repositories;
using Events.ItAcademy.Application.Users.Requests;
using Events.ItAcademy.Application.Users.Responses;
using Events.ItAcademy.Domain.Users;
using Mapster;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.ItAcademy.Application.Users
{
    public class UserService : IUserService
    {
        const string SECRET_KEY = "hardToGuess";
        private readonly IUserRepository _repository;
        private readonly IRoleService _roleService;

        public UserService(IUserRepository repository, IRoleService roleService)
        {
            _repository = repository;
            _roleService = roleService;
        }

        public async Task<string> AuthenticateAsync(CancellationToken cancellationToken, string email, string password)
        {
            var hashed = Hash(password);
            var userEntity = await _repository.GetByEmailAndPassword(cancellationToken, email, hashed);

            if (userEntity == null)
                throw new IncorrectEmailOrPasswordException(ErrorMessages.IncorrectEmailOrPassword);

            return userEntity.Email;
        }


        private string Hash(string input)
        {
            return MyHasher.GenerateHash(input);
        }


        public async Task<string> GetUserIdByEmail(CancellationToken cancellationToken, string name)
        {
            try
            {
                return await _repository.GetUserIdByEmail(cancellationToken, name);
            }
            catch (Exception)
            {
                throw new UserNotFoundException(ErrorMessages.UserNotFound);
            }
        }

        public async Task GiveModerator(CancellationToken token, string userId)
        {
            await _roleService.UpdateRole(token, userId);
        }
        public async Task CreateAsync(CancellationToken cancellation, UserCreateRequestModel userModel)
        {
            var exists = await _repository.ExistsByEmail(cancellation, userModel.Email);

            if (exists)
                throw new UserAlreadyExistsException(ErrorMessages.UserAlreadyExists);
            userModel.Password = Hash(userModel.Password);

            var userEntity = userModel.Adapt<User>();
            userEntity.UserName = userEntity.Email;
            userEntity.NormalizedEmail = userEntity.Email.ToUpper();
            userEntity.NormalizedUserName = userEntity.Email.ToUpper();
            userEntity.LockoutEnabled = true;

            var id = await _repository.CreateAsync(cancellation, userEntity);

            await _roleService.InitializeUserRole(cancellation, id);
        }


        public async Task<List<UserResponseModel>> GetAllUsers(CancellationToken cancellationToken)
        {
            return (await _repository.GetAllAsync(cancellationToken)).Adapt<List<UserResponseModel>>();
        }
    }
}
