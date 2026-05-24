using Contacts.Domain.Models;

namespace Contacts.Application.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetContactsAsync();
        Task<Contact> GetContactByIdAsync(Guid id);
        Task<Contact?> GetByMobilePhoneAsync(string mobilePhone);
        Task<Contact> CreateContactAsync(Contact contact);
        Task<Contact> UpdateContactAsync(Contact contact);
        Task<Contact> DeleteContactAsync(Guid id);
    }
}
