using BookingService.Application.DTOs.Client;
using FluentValidation;

namespace BookingService.Application.Validators.Client
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientDTO>
    {
        public UpdateClientValidator()
        {
            RuleFor(insertClientDTO => insertClientDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertClientDTO.Name)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);
        }
    }
}
