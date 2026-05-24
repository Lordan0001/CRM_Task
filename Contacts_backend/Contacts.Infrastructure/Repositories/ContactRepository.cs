
using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Models;
using Contacts.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly MainContext _context;

        public ContactRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            return await _context.Contacts.AsNoTracking().ToListAsync();
        }
        public async Task<Contact?> GetContactByIdAsync(Guid id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Contact?> GetByMobilePhoneAsync(string mobilePhone)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.MobilePhone == mobilePhone);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> DeleteContactAsync(Guid id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

    }
}
