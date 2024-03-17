using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.DTOs.Restaurant;
using FluentValidation;

namespace CatalogService.Application.Validators.Restaurant
{
    public class InsertRestaurantValidator : AbstractValidator<InsertRestaurantDTO>
    {
        public InsertRestaurantValidator()
        {
            RuleFor(insertRestaurantDTO => insertRestaurantDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertRestaurantDTO.Name)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);

            RuleFor(insertRestaurantDTO => insertRestaurantDTO.City).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertRestaurantDTO.City)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);

            RuleFor(insertRestaurantDTO => insertRestaurantDTO.House).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertRestaurantDTO.House)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);

            RuleFor(insertRestaurantDTO => insertRestaurantDTO.Street).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertRestaurantDTO.Street)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);
        }
    }
}
