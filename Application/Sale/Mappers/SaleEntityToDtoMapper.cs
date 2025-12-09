using Application.Abstractions;
using Application.Sale.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sale.Mappers
{
    public class SaleEntityToDtoMapper : IMapper<SaleEntity, SaleDto>
    {
        public SaleDto Map(SaleEntity saleEntity)
        {
            if(saleEntity == null) 
                throw new ArgumentNullException(nameof(saleEntity));

            var saleDto = new SaleDto
            {
                Id = saleEntity.Id,
                Date = saleEntity.Date,
                Details = saleEntity.Details?.Select(d => new SaleDetailDto
                {
                    Id = d.Id,
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    SaleId = d.SaleId
                }).ToList() ?? new List<SaleDetailDto>()
            };

            return saleDto;
        }
    }
}
