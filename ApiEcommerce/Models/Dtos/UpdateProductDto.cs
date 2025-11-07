using System;

namespace ApiEcommerce.Models.Dtos;

public class UpdateProductDto : CreateProductDto
{
    public int ProductId { get; set; }
}
