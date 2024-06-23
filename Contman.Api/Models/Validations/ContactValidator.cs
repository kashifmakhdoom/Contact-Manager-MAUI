using FluentValidation;
using System;

namespace Contman.Api.Models.Validations
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(m => m.Name).NotEmpty().MinimumLength(3).WithMessage("Contact must not be empty or contact should be at least 3 chars!");
            RuleFor(m => m.Email).NotEmpty().EmailAddress().WithMessage("Email must be valid!");
            RuleFor(m => m.Phone).NotEmpty().WithMessage("Phone must not be empty!");
        }
    }
}
