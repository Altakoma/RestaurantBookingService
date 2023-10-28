using BookingService.Application.Validators;
using FluentValidation;
using OrderService.Application.DTOs.Order;

namespace OrderService.Application.Validators.Order
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderValidator()
        {
            RuleFor(updateOrderDTO => updateOrderDTO.ClientId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.ClientId)));

            RuleFor(updateOrderDTO => updateOrderDTO.MenuId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.MenuId)));

            RuleFor(updateOrderDTO => updateOrderDTO.TableId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.TableId)));

            RuleFor(updateOrderDTO => updateOrderDTO.BookingId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.NotEnteredPropertyMessage, nameof(InsertOrderDTO.BookingId)));
        }
    }
}
