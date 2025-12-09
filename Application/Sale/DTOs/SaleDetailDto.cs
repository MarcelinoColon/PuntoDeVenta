using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sale.DTOs
{
    public class SaleDetailDto
    {
        public int? Id { get; set; }
        public int? SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
