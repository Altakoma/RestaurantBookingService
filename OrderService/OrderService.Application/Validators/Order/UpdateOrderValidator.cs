using FluentValidation;
using OrderService.Application.DTOs.Order;

namespace OrderService.Application.Validators.Order
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderValidator()
        {
            RuleFor(updateOrderDTO => updateOrderDTO.MenuId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.MenuId)));
        }
    }
}
