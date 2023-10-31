using BookingService.Application.DTOs.Table;
using FluentValidation;

namespace BookingService.Application.Validators.Table
{
    public class InsertTableValidator : AbstractValidator<InsertTableDTO>
    {
        public InsertTableValidator()
        {
            RuleFor(insertTableDTO => insertTableDTO.RestaurantId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertTableDTO.RestaurantId)));

            RuleFor(insertTableDTO => insertTableDTO.SeatsCount).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertTableDTO.SeatsCount)));
        }
    }
}
