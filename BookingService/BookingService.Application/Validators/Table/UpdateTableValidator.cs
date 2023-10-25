using BookingService.Application.DTOs.Table;
using FluentValidation;

namespace BookingService.Application.Validators.Table
{
    public class UpdateTableValidator : AbstractValidator<UpdateTableDTO>
    {
        public UpdateTableValidator()
        {
            RuleFor(insertTableDTO => insertTableDTO.RestaurantId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(UpdateTableDTO.RestaurantId)));

            RuleFor(insertTableDTO => insertTableDTO.SeatsCount).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(UpdateTableDTO.SeatsCount)));
        }
    }
}
