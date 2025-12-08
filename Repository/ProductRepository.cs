using Application.Abstractions;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Repository
{
    public class ProductRepository : IReadRepository<ProductEntity>, ICreateRepository<ProductEntity>,
        IUpdateRepository<ProductEntity>, IDeleteRepository
    {
        private StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<ProductEntity> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null)
                throw new KeyNotFoundException($"Producto no existe {id}");

            return MapToEntity(product);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();

            return products.Select(p => MapToEntity(p));
        }

        public async Task AddAsync(ProductEntity productEntity)
        {
            var product = MapToModel(productEntity);
            product.Date = DateTime.UtcNow;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductEntity entity, int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                throw new KeyNotFoundException($"El producto con Id: {id} no existe.");

            MapToModel(entity, product);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"El producto con Id: {id} no existe.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        #region Mapper

        private static ProductEntity MapToEntity(Product model)
        {
            return new ProductEntity(model.Id, model.Name, model.Cost, model.Price, model.Active, model.BrandId);
        }

        private static Product MapToModel(ProductEntity entity)
        {
            return new Product
            {
                Name =  entity.Name,
                Cost = entity.Cost,
                Price = entity.Price,
                Active = entity.Active,
                BrandId = (int)entity.BrandId
            };
        }

        private static void MapToModel(ProductEntity entity, Product model)
        {
            model.Name = entity.Name;
            model.Cost = entity.Cost;
            model.Price = entity.Price;
            model.Active = entity.Active;
            model.BrandId = (int)entity.BrandId;
        }

        #endregion
    }
}
