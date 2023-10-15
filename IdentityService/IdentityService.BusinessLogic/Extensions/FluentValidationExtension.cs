using FluentValidation;
using FluentValidation.Results;

namespace IdentityService.BusinessLogic.Extensions
{
    public static class FluentValidationExtension
    {
        public static async Task ValidateAndThrowArgumentExceptionAsync<T>(this IValidator<T> validator,
            T instance, CancellationToken cancellationToken)
        {
            ValidationResult result = await validator.ValidateAsync(instance, cancellationToken);

            if (!result.IsValid)
            {
                var ex = new ValidationException(result.Errors);

                throw new ArgumentException(ex.Message, ex);
            }
        }
    }
}
