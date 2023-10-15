using FluentValidation;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.DataAccess.Exceptions;

namespace IdentityService.BusinessLogic.Validatiors.UserValidators
{
    internal class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(updateUserDTO => updateUserDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ExceptionMessages.NotEnteredPropertyMessage,nameof(UpdateUserDTO.Name)))
                .MaximumLength(ValidationValues.MaxAccountNameLength);

            RuleFor(updateUserDTO => updateUserDTO.Login).NotEmpty()
                .WithMessage(
                string.Format(ExceptionMessages.NotEnteredPropertyMessage, nameof(UpdateUserDTO.Login)))
                .MaximumLength(ValidationValues.MaxAccountLoginLength);

            RuleFor(updateUserDTO => updateUserDTO.Password).NotEmpty()
                .WithMessage(
                string.Format(ExceptionMessages.NotEnteredPropertyMessage, nameof(UpdateUserDTO.Password)))
                .MaximumLength(ValidationValues.MaxAccountPasswordLength);

            RuleFor(updateUserDTO => updateUserDTO.UserRoleId).NotEqual(0)
                .WithMessage(
                string.Format(ExceptionMessages.DetectionZeroPropertyMessage, nameof(UpdateUserDTO.UserRoleId)));
        }
    }
}
