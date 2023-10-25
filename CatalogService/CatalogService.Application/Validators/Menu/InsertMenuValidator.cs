using CatalogService.Application.DTOs.Menu;
using FluentValidation;

namespace CatalogService.Application.Validators.Menu
{
    public class InsertMenuValidator : AbstractValidator<InsertMenuDTO>
    {
        public InsertMenuValidator()
        {
            RuleFor(insertMenuDTO => insertMenuDTO.RestaurantId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertMenuDTO.RestaurantId)));

            RuleFor(insertMenuDTO => insertMenuDTO.Cost).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertMenuDTO.Cost)));

            RuleFor(insertMenuDTO => insertMenuDTO.FoodTypeId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertMenuDTO.FoodTypeId)));

            RuleFor(insertMenuDTO => insertMenuDTO.FoodName).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertMenuDTO.FoodName)))
                .MaximumLength(ValidationResources.MaximumLengthForMiddleLengthColumn);
        }
    }
}
