
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using Mapster;

namespace ApiEcommerce.Mapping;

public static class CategoryMappingConfig
{
    public static void Register()
    {
        TypeAdapterConfig<Category, CategoryDto>.NewConfig();
        TypeAdapterConfig<Category, CreateCategoryDto>.NewConfig();
        // ReverseMap no es necesario, Mapster soporta bidireccional por defecto si las propiedades coinciden
    }
}