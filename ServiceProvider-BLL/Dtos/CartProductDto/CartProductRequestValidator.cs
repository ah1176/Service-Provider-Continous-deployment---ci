using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.CartProductDto
{
    public class CartProductRequestValidator : AbstractValidator<CartProductRequest>
    {
        public CartProductRequestValidator() 
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.CartId)
           .GreaterThan(0).WithMessage("Cart ID must be greater than 0.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        }
    }
}
