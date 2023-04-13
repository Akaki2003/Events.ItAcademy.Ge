using Events.ItAcademy.Application.Events.Repositories;
using Events.ItAcademy.Application.Events;
using Events.ItAcademy.Application.EventsSetups.Repositories;
using Events.ItAcademy.Application.UserTickets.Repositories;
using Events.ItAcademy.Application.UserTickets;
using Events.ItAcademy.Domain.Events;
using Events.ItAcademy.Domain.EventSetup;
using Events.ItAcademy.Domain.UserTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.ItAcademy.Application.Events.Requests;
using Events.ItAcademy.Application.Users.Repositories;
using Events.ItAcademy.Application.Users;
using Events.ItAcademy.Application.Roles;
using Events.ItAcademy.Infrastructure.Roles;
using Events.ItAcademy.Domain.Users;
using Events.ItAcademy.Application.Exceptions.Tickets;
using Events.ItAcademy.Application.Exceptions.Users;
using Xunit.Sdk;
using Events.ItAcademy.Application.Roles.Repositories;
using Moq;
using Events.ItAcademy.Application.Users.Requests;
using Mapster;

namespace EventsItAcademyTest.Users
{
    public class UserTests
    {
        private User _userDomain;
        private readonly Mock<IUserRepository> userMockRepo = new Mock<IUserRepository> { DefaultValue = DefaultValue.Empty };
        private readonly Mock<IRoleRepository> roleMockRepo = new Mock<IRoleRepository> { DefaultValue = DefaultValue.Empty };
        private readonly Mock<IEventRepository> eventMockRepo = new Mock<IEventRepository> { DefaultValue = DefaultValue.Empty };
        private readonly Mock<IEventSetupRepository> setupMockRepo = new Mock<IEventSetupRepository> { DefaultValue = DefaultValue.Empty };
        private readonly new Mock<IUserTicketRepository> ticketMockRepo = new Mock<IUserTicketRepository> { DefaultValue = DefaultValue.Empty };
        private readonly CancellationToken cancellationToken = new CancellationToken();
        private readonly UserService userService;
        private readonly RoleService roleService;

        public UserTests()
        {
            roleService = new RoleService(roleMockRepo.Object);
            userService = new UserService(userMockRepo.Object, roleService);
            _userDomain = GetUser();
        }

        [Fact]
        private async Task WhenUserIsNull_ShouldThrowIncorrectEmailOrPasswordExceptionException()
        {
            //arrange
            _userDomain = null;
            userMockRepo.Setup(x => x.GetByEmailAndPassword(cancellationToken, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(_userDomain);

            //act
            var test = async () => await userService.AuthenticateAsync(cancellationToken, It.IsAny<string>(), It.IsAny<string>());


            //assert
            await Assert.ThrowsAsync<IncorrectEmailOrPasswordException>(test);
        }


        [Fact]
        private async Task WhenUserIsNotFound_ShouldThrowUserNotFoundException()
        {
            //arrange
            _userDomain = null;
            userMockRepo.Setup(x => x.GetUserIdByEmail(cancellationToken, It.IsAny<string>())).ThrowsAsync(new UserNotFoundException());

            //act
            var test = async () => await userService.GetUserIdByEmail(cancellationToken, It.IsAny<string>());


            //assert
            await Assert.ThrowsAsync<UserNotFoundException>(test);
        }

        [Fact]
        private async Task WhenUserAlreadyExistsShouldThrowUserAlreadyExistsException()
        {
            //arrange
            userMockRepo.Setup(x => x.ExistsByEmail(cancellationToken, It.IsAny<string>())).ReturnsAsync(true);

            //act
            var test = async () => await userService.CreateAsync(cancellationToken, _userDomain.Adapt<UserCreateRequestModel>());


            //assert
            await Assert.ThrowsAsync<UserAlreadyExistsException>(test);
        }

       
        private User GetUser()
        {
            User userTicket = new User()
            {
                AccessFailedCount = 1,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                SecurityStamp = "d",
                ConcurrencyStamp = "d",
                Email = "mail",
                EmailConfirmed = true,
                Id = "d",
                LockoutEnabled = false,
                LockoutEnd = DateTime.Now,
                NormalizedEmail = "D",
                NormalizedUserName = "D",
                PasswordHash = "D",
                PhoneNumber = "9",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                UserName = "D"
            };
            return userTicket;
        }
    }
}
