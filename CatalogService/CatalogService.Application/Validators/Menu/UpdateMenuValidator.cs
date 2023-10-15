using CatalogService.Application.DTOs.Menu;
using FluentValidation;

namespace CatalogService.Application.Validators.Menu
{
    public class UpdateMenuValidator : AbstractValidator<UpdateMenuDTO>
    {
        public UpdateMenuValidator()
        {
            RuleFor(updateMenuDTO => updateMenuDTO.RestaurantId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(UpdateMenuDTO.RestaurantId)));

            RuleFor(updateMenuDTO => updateMenuDTO.Cost).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(UpdateMenuDTO.Cost)));

            RuleFor(updateMenuDTO => updateMenuDTO.FoodTypeId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(UpdateMenuDTO.FoodTypeId)));

            RuleFor(updateMenuDTO => updateMenuDTO.FoodName).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(UpdateMenuDTO.FoodName)))
                .MaximumLength(ValidationResources.MaximumLengthForMiddleLengthColumn);
        }
    }
}
