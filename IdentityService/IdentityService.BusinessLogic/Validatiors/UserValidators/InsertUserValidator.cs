using FluentValidation;
using IdentityService.BusinessLogic.DTOs.UserDTOs;
using IdentityService.BusinessLogic.Exceptions;

namespace IdentityService.BusinessLogic.Validatiors.UserValidators
{
    public class InsertUserValidator : AbstractValidator<InsertUserDTO>
    {
        public InsertUserValidator()
        {
            RuleFor(u => u.Name).NotEmpty()
                .WithMessage(ExceptionMessages.NotEnteredProperty)
                .MaximumLength(ValidationValues.MaxAccountNameLength);

            RuleFor(u => u.Login).NotEmpty()
                .WithMessage(ExceptionMessages.NotEnteredProperty)
                .MaximumLength(ValidationValues.MaxAccountLoginLength);

            RuleFor(u => u.Password).NotEmpty()
                .WithMessage(ExceptionMessages.NotEnteredProperty)
                .MaximumLength(ValidationValues.MaxAccountPasswordLength);
        }
    }
}
