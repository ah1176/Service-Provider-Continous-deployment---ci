using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.CategoryDto
{
    public class SubCategoryRequestValidator : AbstractValidator<SubCategoryRequest>
    {
        public SubCategoryRequestValidator() 
        {
            RuleFor(x => x.NameEn).NotEmpty().MaximumLength(100);
            RuleFor(x => x.NameAr).NotEmpty().MaximumLength(100);
        }
    }
}
