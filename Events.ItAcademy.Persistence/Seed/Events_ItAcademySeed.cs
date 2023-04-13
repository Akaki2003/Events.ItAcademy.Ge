using Events.ItAcademy.Domain.EventSetup;
using Events.ItAcademy.Domain.Users;
using Events.ItAcademy.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace Events.ItAcademy.Persistence.Seed
{
    public static class Events_ItAcademySeed
    {
        private static string SECRET_KEY = "hardToGuess";
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<EventsItAcademyContext>();

            Migrate(database);
            SeedEverything(database);
        }

        private static void Migrate(EventsItAcademyContext context)
        {
            context.Database.Migrate();
        }

        private static void SeedEverything(EventsItAcademyContext context)
        {
            var seeded = false;

            SeedUsers(context, ref seeded);

            if (seeded)
                context.SaveChanges();
        }

        private static void SeedUsers(EventsItAcademyContext context, ref bool seeded)
        {
            var id = new Guid().ToString();
            var Username = "Admin@gmail.com";
            var password = GenerateHash("Akaki!1");
            var users = new List<User>()
            {
                new User
                {
                    Id = id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt= DateTime.Now,
                    UserName = Username,
                    Email = Username,
                    NormalizedUserName =  Username.ToUpper(),
                    NormalizedEmail =  Username.ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = password,
                    SecurityStamp = "securityStamp",
                    ConcurrencyStamp = "concurrencyStamp",
                    PhoneNumber = "999999999",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    //LockoutEnd = null
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },

            };
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "askjdhfkjhskfj"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Moderator",
                    NormalizedName = "MODERATOR",
                    ConcurrencyStamp = "askjdhfkjhskfj"
                },
                  new IdentityRole
                {
                    Id = "3",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "askjdhfkjhskfj"
                },
                    //new IdentityRole
                //{
                //    Id = "4",
                //    Name = "Guest",
                //    NormalizedName = "GUEST",
                //    ConcurrencyStamp = "askjdhfkjhskfj"
                //},
            };

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    UserId = id,
                    RoleId = "1",
                },
            };


            foreach (var user in users)
            {
                if (context.Users.Any(x => x.UserName == user.UserName)) continue;

                context.Users.Add(user);

                seeded = true;
            }
            foreach (var role in roles)
            {
                if (context.Roles.Any(x => x.Id == role.Id)) continue;

                context.Roles.Add(role);

                seeded = true;
            }
            foreach (var userRole in userRoles)
            {
                if (context.UserRoles.Any(x => x.UserId == userRole.UserId)) continue;

                context.UserRoles.Add(userRole);

                seeded = true;
            }


            var eventSettings = new EventSetup
            {
                EditEventAfterUploadInDays = 3,
                ReserveTimeLengthInMinutes = 3
            };
            if (!context.EventSetups.Any())
            {
                context.EventSetups.Add(eventSettings);
            }



            

        }
        static string GenerateHash(string input)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + SECRET_KEY);
                byte[] hashBytes = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
