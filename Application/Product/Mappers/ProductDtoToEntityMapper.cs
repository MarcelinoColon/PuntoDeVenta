using Application.Abstractions;
using Application.Product.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Product.Mappers
{
    public class ProductDtoToEntityMapper : IMapper<ProductDto, ProductEntity>
    {
        public ProductEntity Map(ProductDto productDto)
        {
            return productDto.Id == null ?
                 new ProductEntity(productDto.Name,productDto.Cost, productDto.Price, productDto.Active, productDto.BrandId)
                 :
                 new ProductEntity(productDto.Id, productDto.Name, productDto.Cost, productDto.Price, productDto.Active, productDto.BrandId);
        }
    }
}
