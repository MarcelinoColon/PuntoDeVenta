using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace Application.Sale.DTOs
{
    public class SaleDto
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public List<SaleDetailDto> Details { get; set; } = new();
        public decimal Total => Details?.Sum(p => p.TotalPrice) ?? 0;
    }
}
