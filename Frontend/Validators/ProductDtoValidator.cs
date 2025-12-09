using FluentValidation;
using Frontend.Client;

namespace Frontend.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("Maximo 100 caracteres")
                .MinimumLength(3).WithMessage("Minimo 3 caracteres");

            RuleFor(p => p.Cost)
                .GreaterThan(0).WithMessage("El costo debe ser mayor a $ 0");

            RuleFor(p=> p.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a $ 0");
            RuleFor(p => p.Price)
                .Must((product, price) => price > product.Cost).WithMessage("El precio debe ser mayor al costo");
        }
    }
}
