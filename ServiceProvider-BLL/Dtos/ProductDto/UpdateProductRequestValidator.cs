using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.ProductDto
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator() 
        {
            RuleFor(x => x.NameEn).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.NameEn));
            RuleFor(x => x.NameAr).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.NameAr));
            RuleFor(x => x.Price).GreaterThan(0).When(x => x.Price > 0);
        }
    }
}
