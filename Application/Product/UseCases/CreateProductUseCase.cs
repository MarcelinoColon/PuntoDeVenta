using Application.Abstractions;
using Application.Product.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Product.UseCases
{
    public class CreateProductUseCase : ICreateUseCase<ProductDto, ProductEntity>
    {
        private readonly ICreateRepository<ProductEntity> _repository;
        private readonly IMapper<ProductDto, ProductEntity> _mapper;

        public CreateProductUseCase(ICreateRepository<ProductEntity> repository, IMapper<ProductDto, ProductEntity> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAsync(ProductDto productDto)
        {
            var productEntity = _mapper.Map(productDto);
            await _repository.AddAsync(productEntity);
        }
    }
}
