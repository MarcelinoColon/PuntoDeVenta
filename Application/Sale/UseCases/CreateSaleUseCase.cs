using Application.Abstractions;
using Application.Sale.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sale.UseCases
{
    public class CreateSaleUseCase : ICreateUseCase<SaleDto, SaleEntity>
    {
        private readonly ICreateRepository<SaleEntity> _repository;
        private readonly IMapper<SaleDto, SaleEntity> _mapper;
        public CreateSaleUseCase(ICreateRepository<SaleEntity> repository, IMapper<SaleDto, SaleEntity> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAsync(SaleDto saleDto)
        {
            if(saleDto == null)
                throw new ArgumentNullException(nameof(SaleDto));

            var saleEntity = _mapper.Map(saleDto);
            saleEntity.Validate();

            await _repository.AddAsync(saleEntity);
        }
    }
}
