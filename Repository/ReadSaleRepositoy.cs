using Application.Abstractions;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ReadSaleRepositoy : IReadRepository<SaleEntity>
    {
        private readonly StoreContext _context;

        public ReadSaleRepositoy(StoreContext context)
        {
            _context = context;
        }

        public async Task<SaleEntity> GetByIdAsync(int id)
        {
            if(id <= 0)
                throw new ArgumentException("Id debe ser mayor a 0.", nameof(id));

            var sale = await _context.Sales.Include(s => s.SaleDetails)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                throw new KeyNotFoundException($"Venta con Id {id} no existe.");

            return MapToEntity(sale);
        }

        public async Task<IEnumerable<SaleEntity>> GetAllAsync()
        {
            var sales = await _context.Sales.Include(s => s.SaleDetails)
                .ToListAsync();

            return sales.Select(s => MapToEntity(s));
        }



        #region Mappers
        public static SaleEntity MapToEntity(Sale model)
        {
            var saleEntity = new SaleEntity(model.Date, model.Id);

            foreach(var saleDetail in model.SaleDetails)
            {
                saleEntity.AddDetail(new SaleDetailEntity(
                    model.Id, 
                    saleDetail.ProductId,
                    saleDetail.Quantity,
                    saleDetail.UnitPrice,
                    saleDetail.Id));
            }

            return saleEntity;
        }
        #endregion
    }
}
