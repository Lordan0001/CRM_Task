namespace Contacts.Application.Exceptions
{
    public class DuplicateContactException : DomainException
    {
        public DuplicateContactException(string phone) : base($"Contact with phone number '{phone}' already exists.")
        {
        }
    }
}
