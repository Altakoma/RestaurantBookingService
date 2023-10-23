using BookingService.Application.DTOs.Restaurant;
using BookingService.Application.Validators;
using FluentValidation;

namespace CatalogService.Application.Validators.Restaurant
{
    public class InsertRestaurantValidator : AbstractValidator<InsertRestaurantDTO>
    {
        public InsertRestaurantValidator()
        {
            RuleFor(insertRestaurantDTO => insertRestaurantDTO.Id).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertRestaurantDTO.Id)));

            RuleFor(insertRestaurantDTO => insertRestaurantDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertRestaurantDTO.Name)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);
        }
    }
}
