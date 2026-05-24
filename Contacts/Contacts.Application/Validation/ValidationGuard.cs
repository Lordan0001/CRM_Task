using Contacts.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace Contacts.Application.Validation
{
    public static class ValidationGuard
    {
        public static void Validate<TDto>(TDto dto, IValidator<TDto> validator)
        {
            ValidationResult result = validator.Validate(dto);
            if (!result.IsValid)
                throw new DomainValidationException(result.Errors.Select(e => e.ErrorMessage));
        }
    }
}
