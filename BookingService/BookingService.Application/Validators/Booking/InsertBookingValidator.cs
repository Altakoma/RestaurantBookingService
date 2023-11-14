using BookingService.Application.DTOs.Booking;
using FluentValidation;

namespace BookingService.Application.Validators.Booking
{
    public class InsertBookingValidator : AbstractValidator<InsertBookingDTO>
    {
        public InsertBookingValidator()
        {
            RuleFor(insertBookingDTO => insertBookingDTO.TableId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertBookingDTO.TableId)));

            RuleFor(insertBookingDTO => insertBookingDTO.BookingTime).GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage(ValidationResources.ProvidedDataIsNotValid);
        }
    }
}
