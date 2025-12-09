using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SaleDetailEntity
    {
        public int? Id { get; private set; }
        public int? SaleId { get;private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public SaleDetailEntity(int? saleId, int productId, int quantity, decimal unitPrice, int? id = null)
        {
            if (id.HasValue && id <= 0)
                throw new ArgumentException("Id debe ser mayor a 0.", nameof(id));
            if (saleId.HasValue && id <= 0)
                throw new ArgumentException("El id de la venta debe ser mayor a 0.", nameof(saleId));
            if (productId <= 0)
                throw new ArgumentException("El id del producto debe ser mayor a 0.", nameof(productId));
            if (quantity <= 0)
                throw new ArgumentException("Cantidad debe ser mayor a 0.", nameof(quantity));
            if (unitPrice < 0)
                throw new ArgumentException("Precio unitario debe ser mayor a 0.", nameof(unitPrice));

            Id = id;
            SaleId = saleId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        private void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Cantidad debe ser mayor a 0.", nameof(quantity));
            Quantity = quantity;
        }

        private void UpdateUnitPrice(decimal unitPrice)
        {
            if (unitPrice < 0)
                throw new ArgumentException("Precio unitario debe ser mayor a 0.", nameof(unitPrice));
            UnitPrice = unitPrice;
        }
    }
}
