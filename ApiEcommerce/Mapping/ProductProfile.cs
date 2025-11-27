
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using Mapster;

namespace ApiEcommerce.Mapping;

public static class ProductMappingConfig
{
    public static void Register()
    {
        TypeAdapterConfig<Product, ProductDto>
            .NewConfig()
            .Map(dest => dest.CategoryName, src => src.Category.Name);
        TypeAdapterConfig<Product, CreateProductDto>.NewConfig();
        TypeAdapterConfig<Product, UpdateProductDto>.NewConfig();
        // ReverseMap no es necesario, Mapster soporta bidireccional por defecto si las propiedades coinciden
    }
}
