using Contacts.Application.Dto;
using FluentValidation;

namespace Contacts.Application.Validation
{
    public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
    {
        public CreateContactDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.MobilePhone)
                .NotEmpty()
                .MaximumLength(13);

            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Today)
                .WithMessage("Birth date must be in the past.");
        }
    }
}
