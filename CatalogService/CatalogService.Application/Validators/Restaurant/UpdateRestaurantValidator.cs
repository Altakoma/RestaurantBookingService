using CatalogService.Application.DTOs.Restaurant;
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

            RuleFor(updateRestaurantDTO => updateRestaurantDTO.City).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(UpdateRestaurantDTO.City)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);

            RuleFor(updateRestaurantDTO => updateRestaurantDTO.House).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(UpdateRestaurantDTO.House)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);

            RuleFor(updateRestaurantDTO => updateRestaurantDTO.Street).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(UpdateRestaurantDTO.Street)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);
        }
    }
}
