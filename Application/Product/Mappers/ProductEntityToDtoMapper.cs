using Application.Abstractions;
using Application.Product.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Product.Mappers
{
    public class ProductEntityToDtoMapper : IMapper<ProductEntity, ProductDto>
    {
        public ProductDto Map(ProductEntity productEntity)
        {
            if(productEntity == null)
                throw new ArgumentNullException(nameof(productEntity));

            return new ProductDto
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Cost = productEntity.Cost,
                Price = productEntity.Price,
                Active = productEntity.Active,
                BrandId = productEntity.BrandId
            };
        }
    }
}
