using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Dtos.VendorDto
{
    public class ChangeVendorPasswordRequestValidator : AbstractValidator<ChangeVendorPasswordRequest>
    {
        public ChangeVendorPasswordRequestValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
               .Matches("(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}")
               .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase")
               .NotEqual(x => x.CurrentPassword)
               .WithMessage("New Password cannot be the same as current password");
        }
    }
}
