using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductEntity
    {
        private int? _id;
        private string _name;
        private decimal _cost;
        private decimal _price;
        private bool _active;
        private int? _brandId;


        #region properties
        public int? Id
        {
            get => _id;
            set {
                if(value <= 0)
                    throw new ArgumentException("Id debe ser mayor que cero.", nameof(value));

                _id = value;
            }
        }

        public string Name
        {
            get => _name;
            set {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no puede estar vacío.", nameof(value));
                if(value.Length > 100)
                    throw new ArgumentException("El nombre no puede tener más de 100 caracteres.", nameof(value));

                _name = value;
            }
        }

        public decimal Cost
        {
            get => _cost;
            set {
                if(value < 0)
                    throw new ArgumentException("El costo no puede ser negativo.", nameof(value));

                _cost = value;
            }
        }

        public decimal Price
        {
            get => _price;
            set {
                if(value < 0)
                    throw new ArgumentException("El precio no puede ser negativo.", nameof(value));
                if(value < _cost)
                    throw new ArgumentException("El precio no puede ser menor que el costo.", nameof(value));

                _price = value;
            }
        }

        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        public int? BrandId
        {
            get => _brandId;
            set {
                if(value <= 0)
                    throw new ArgumentException("BrandId debe ser mayor que cero.", nameof(value));

                _brandId = value;
            }
        }

        #endregion

        public ProductEntity(int? id, string name, decimal cost, decimal price, bool active, int? brandId)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Price = price;
            Active = active;
            BrandId = brandId;
        }

        public ProductEntity(string name, decimal cost, decimal price, bool active, int? brandId)
        {
            Name = name;
            Cost = cost;
            Price = price;
            Active = active;
            BrandId = brandId;
        }

    }
}
