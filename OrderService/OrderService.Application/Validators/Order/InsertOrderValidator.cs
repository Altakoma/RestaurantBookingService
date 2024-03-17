using FluentValidation;
using OrderService.Application.DTOs.Order;

namespace OrderService.Application.Validators.Order
{
    public class InsertOrderValidator : AbstractValidator<InsertOrderDTO>
    {
        public InsertOrderValidator()
        {
            RuleFor(insertOrderDTO => insertOrderDTO.MenuId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.MenuId)));

            RuleFor(insertOrderDTO => insertOrderDTO.BookingId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.BookingId)));
        }
    }
}
