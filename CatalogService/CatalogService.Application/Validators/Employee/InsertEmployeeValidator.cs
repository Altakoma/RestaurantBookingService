using CatalogService.Application.DTOs.Employee;
using FluentValidation;

namespace CatalogService.Application.Validators.Employee
{
    public class InsertEmployeeValidator : AbstractValidator<InsertEmployeeDTO>
    {
        public InsertEmployeeValidator()
        {
            RuleFor(insertEmployeeDTO => insertEmployeeDTO.Id).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertEmployeeDTO.Id)));

            RuleFor(insertEmployeeDTO => insertEmployeeDTO.RestaurantId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertEmployeeDTO.RestaurantId)));

            RuleFor(insertEmployeeDTO => insertEmployeeDTO.Name).NotEmpty()
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertEmployeeDTO.Name)))
                .MaximumLength(ValidationResources.MaximumLengthForSmallLengthColumn);
        }
    }
}
