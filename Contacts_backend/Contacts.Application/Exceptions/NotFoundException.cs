namespace Contacts.Application.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string resource, object? key = null)
            : base($"{resource} with id '{key}' was not found.")
        {
        }
    }
}