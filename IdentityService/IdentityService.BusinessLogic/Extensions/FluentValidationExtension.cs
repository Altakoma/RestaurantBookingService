using FluentValidation;
using FluentValidation.Results;

namespace IdentityService.BusinessLogic.Extensions
{
    public static class FluentValidationExtension
    {
        public static async Task ValidateAndThrowArgumentException<T>(this IValidator<T> validator, T instance)
        {
            ValidationResult result = await validator.ValidateAsync(instance);

            if (!result.IsValid)
            {
                var ex = new ValidationException(result.Errors);

                throw new ArgumentException(ex.Message, ex);
            }
        }
    }
}
