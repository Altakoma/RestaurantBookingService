using CatalogService.Application.DTOs.Employee;
using FluentValidation;

namespace CatalogService.Application.Validators.Employee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDTO>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(updateEmployeeDTO => updateEmployeeDTO.RestaurantId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(UpdateEmployeeDTO.RestaurantId)));

            RuleFor(updateEmployeeDTO => updateEmployeeDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(UpdateEmployeeDTO.Name)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);
        }
    }
}
