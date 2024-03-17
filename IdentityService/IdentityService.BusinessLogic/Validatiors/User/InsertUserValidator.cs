using FluentValidation;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.DataAccess.Exceptions;

namespace IdentityService.BusinessLogic.Validatiors.User
{
    public class InsertUserValidator : AbstractValidator<InsertUserDTO>
    {
        public InsertUserValidator()
        {
            RuleFor(insertUserDTO => insertUserDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ExceptionMessages.NotEnteredPropertyMessage, nameof(UpdateUserDTO.Name)))
                .MaximumLength(ValidationValues.MaxAccountNameLength);

            RuleFor(insertUserDTO => insertUserDTO.Login).NotEmpty()
                .WithMessage(
                string.Format(ExceptionMessages.NotEnteredPropertyMessage, nameof(UpdateUserDTO.Login)))
                .MaximumLength(ValidationValues.MaxAccountLoginLength);

            RuleFor(insertUserDTO => insertUserDTO.Password).NotEmpty()
                .WithMessage(
                string.Format(ExceptionMessages.NotEnteredPropertyMessage, nameof(UpdateUserDTO.Password)))
                .MaximumLength(ValidationValues.MaxAccountPasswordLength);

            RuleFor(updateUserDTO => updateUserDTO.UserRoleId).NotEqual(ValidationValues.ZeroId)
                .WithMessage(
                string.Format(ExceptionMessages.DetectionZeroPropertyMessage, nameof(UpdateUserDTO.UserRoleId)));
        }
    }
}
