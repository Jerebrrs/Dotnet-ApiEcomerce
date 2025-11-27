
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using Mapster;

namespace ApiEcommerce.Mapping;

public static class UserMappingConfig
{
    public static void Register()
    {
        TypeAdapterConfig<User, UserDto>.NewConfig();
        TypeAdapterConfig<User, CreateUserDto>.NewConfig();
        TypeAdapterConfig<User, UserLoginResponseDto>.NewConfig();
        TypeAdapterConfig<User, UserLoginDto>.NewConfig();
        TypeAdapterConfig<ApplicationUser, UserDataDto>.NewConfig();
        TypeAdapterConfig<ApplicationUser, UserDto>.NewConfig();
        // ReverseMap no es necesario, Mapster soporta bidireccional por defecto si las propiedades coinciden
    }
}
