using Application.Abstractions;
using Application.Product.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Product.UseCases
{
    public class ProductUseCase: IReadUseCase<ProductDto, ProductEntity>
    {
        private readonly IReadRepository<ProductEntity> _repository;
        private readonly IMapper<ProductEntity, ProductDto> _mapper;
        public ProductUseCase(
            IReadRepository<ProductEntity> repository,
            IMapper<ProductEntity, ProductDto> mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var productEntity = await _repository.GetByIdAsync(id);

            if(productEntity == null)
            {
                return null;
            }

            return _mapper.Map(productEntity);
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var productEntities = await _repository.GetAllAsync();

            return productEntities.Select(p => _mapper.Map(p));
        }
    }
}
