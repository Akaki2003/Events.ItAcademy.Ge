using Events.ItAcademy.Application.Users.Requests;
using Events.ItAcademy.Domain.Users;
using Mapster;

namespace Events.ItAcademy.API.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {

        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<UserCreateRequestModel, User>
            .NewConfig().Map(dest => dest.PasswordHash, src => src.Password);
        }

    }
}
