using CatalogService.Application.DTOs.FoodType;
using FluentValidation;

namespace CatalogService.Application.Validators.FoodType
{
    public class FoodTypeDTOValidator : AbstractValidator<FoodTypeDTO>
    {
        public FoodTypeDTOValidator()
        {
            RuleFor(foodTypeDTO => foodTypeDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(FoodTypeDTO.Name)))
                .MaximumLength(ValidationResources.MaximumLengthForMiddleLengthColumn);
        }
    }
}
