using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Product.UseCases
{
    public class DeleteProductUseCase : IDeleteUseCase
    {
        private readonly IDeleteRepository _repository;
        public DeleteProductUseCase(IDeleteRepository repository) 
        {
            _repository = repository;
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
