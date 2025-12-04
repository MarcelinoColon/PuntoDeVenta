using Application.Abstractions;
using Azure.Core;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class BrandRepository : IRepository<BrandEntity>
    {
        private StoreContext _context;

        public BrandRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<BrandEntity> GetByIdAsync(int id) 
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if(brand == null)
                throw new KeyNotFoundException($"La marca no existe");

            return MapToEntity(brand);
        }

        #region
        private static BrandEntity MapToEntity(Brand model)
        {
            return new BrandEntity(model.Id, model.Name);
        }

        private static Brand MapToModel(BrandEntity entity)
        {
            return new Brand
            {
                Id = entity.Id ?? 0,
                Name = entity.Name
            };
        }
        #endregion
    }
}
