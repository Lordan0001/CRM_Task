using Contacts.Application.Dto;

namespace Contacts.Application.Interfaces.Services
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetContactsAsync();
        Task<ContactDto> GetContactByIdAsync(Guid id);
        Task<ContactDto> CreateContactAsync(CreateContactDto contact);
        Task<ContactDto> UpdateContactAsync(Guid id, UpdateContactDto contact);
        Task<ContactDto> DeleteContactAsync(Guid id);
    }
}
