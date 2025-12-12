using FluentValidation;
using Frontend.Client;

namespace Frontend.Validators
{
    public class SaleDtoValidator : AbstractValidator<SaleDto>
    {
        public SaleDtoValidator() 
        {
            RuleFor(x => x.Details)
                .NotEmpty().WithMessage("La venta debe tener productos.");

            RuleForEach(x => x.Details).ChildRules(details =>
            {
                details.RuleFor(x => x.ProductId)
                    .GreaterThan(0).WithMessage("El Id del producto debe ser mayor que cero.");

                details.RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("La cantidad del producto debe ser mayor que cero.");
            });

            RuleFor(x=> x.Date)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La fecha de la venta no puede ser del futuro.");
        }
    }
}
