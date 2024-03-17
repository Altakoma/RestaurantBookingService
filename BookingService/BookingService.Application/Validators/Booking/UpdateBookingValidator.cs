using BookingService.Application.DTOs.Booking;
using FluentValidation;

namespace BookingService.Application.Validators.Booking
{
    public class UpdateBookingValidator : AbstractValidator<UpdateBookingDTO>
    {
        public UpdateBookingValidator()
        {
            RuleFor(updateBookingDTO => updateBookingDTO.TableId).NotEqual(ValidationResources.ZeroId)
                .WithMessage(
                string.Format(ValidationResources.DetectionZeroPropertyMessage, nameof(InsertBookingDTO.TableId)));

            RuleFor(updateBookingDTO => updateBookingDTO.BookingTime).LessThanOrEqualTo(DateTime.Now)
                .WithMessage(ValidationResources.ProvidedDataIsNotValid);
        }
    }
}
