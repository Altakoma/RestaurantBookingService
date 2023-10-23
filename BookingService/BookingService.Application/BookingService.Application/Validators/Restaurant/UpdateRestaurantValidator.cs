using BookingService.Application.DTOs.Restaurant;
using BookingService.Application.Validators;
using FluentValidation;

namespace CatalogService.Application.Validators.Restaurant
{
    public class UpdateRestaurantValidator : AbstractValidator<UpdateRestaurantDTO>
    {
        public UpdateRestaurantValidator()
        {
            RuleFor(updateRestaurantDTO => updateRestaurantDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(UpdateRestaurantDTO.Name)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);
        }
    }
}
