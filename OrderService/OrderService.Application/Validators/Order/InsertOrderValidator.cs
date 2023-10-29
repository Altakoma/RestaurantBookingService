using BookingService.Application.Validators;
using FluentValidation;
using OrderService.Application.DTOs.Order;

namespace OrderService.Application.Validators.Order
{
    public class InsertOrderValidator : AbstractValidator<InsertOrderDTO>
    {
        public InsertOrderValidator()
        {
            RuleFor(insertOrderDTO => insertOrderDTO.ClientId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.ClientId)));

            RuleFor(insertOrderDTO => insertOrderDTO.MenuId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.MenuId)));

            RuleFor(insertOrderDTO => insertOrderDTO.TableId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.TableId)));

            RuleFor(insertOrderDTO => insertOrderDTO.BookingId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertOrderDTO.BookingId)));
        }
    }
}
