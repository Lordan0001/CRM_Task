namespace Contacts.Application.Exceptions
{
    public class DomainValidationException : DomainException
    {
        public IReadOnlyCollection<string> Errors { get; }

        public DomainValidationException(IEnumerable<string> errors) : base("Validation failed")
        {
            Errors = errors.ToList();
        }
    }
}
