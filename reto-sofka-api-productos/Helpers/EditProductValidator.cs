using FluentValidation;
using reto_sofka_api_productos.DTOs;

namespace reto_sofka_api_productos.Helpers
{
    public class EditProductValidator : AbstractValidator<EditProductDTO>
    {
        public EditProductValidator()
        {

            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.InInventory).GreaterThan(0);
            RuleFor(p => p.Min).GreaterThan(0).LessThanOrEqualTo(p => p.InInventory);
            RuleFor(p => p.Max).GreaterThanOrEqualTo(p => p.Min);

        }

    }
}
