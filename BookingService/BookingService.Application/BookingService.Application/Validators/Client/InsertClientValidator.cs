using BookingService.Application.DTOs.Client;
using FluentValidation;

namespace BookingService.Application.Validators.Client
{
    public class InsertClientValidator : AbstractValidator<InsertClientDTO>
    {
        public InsertClientValidator()
        {
            RuleFor(insertClientDTO => insertClientDTO.Id).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertClientDTO.Id)));

            RuleFor(insertClientDTO => insertClientDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertClientDTO.Name)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);
        }
    }
}
