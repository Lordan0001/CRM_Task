using Contacts.Application.Dto;
using Contacts.Application.Exceptions;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Application.Interfaces.Services;
using Contacts.Application.Validation;
using Contacts.Domain.Models;
using FluentValidation;
using MapsterMapper;

namespace Contacts.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;
        private readonly IValidator<CreateContactDto> _createValidator;
        private readonly IValidator<UpdateContactDto> _updateValidator;


        public ContactService(IMapper mapper, IContactRepository contactRepository,
            IValidator<UpdateContactDto> updateValidator, IValidator<CreateContactDto> createValidator)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ContactDto> GetContactByIdAsync(Guid id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id) ?? throw new NotFoundException("Contact", id);

            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<List<ContactDto>> GetContactsAsync()
        {
            var contacts = await _contactRepository.GetContactsAsync();

            return _mapper.Map<List<ContactDto>>(contacts);
        }

        public async Task<ContactDto> CreateContactAsync(CreateContactDto contactDto)
        {
            ValidationGuard.Validate(contactDto, _createValidator);

            var existingContact = await _contactRepository.GetByMobilePhoneAsync(contactDto.MobilePhone);
            if (existingContact is not null)
            {
                throw new DuplicateContactException(contactDto.MobilePhone);
            }

            var contact = _mapper.Map<Contact>(contactDto);
            var createdContact = await _contactRepository.CreateContactAsync(contact);

            return _mapper.Map<ContactDto>(createdContact);
        }

        public async Task<ContactDto> UpdateContactAsync(Guid id, UpdateContactDto contactDto)
        {
            ValidationGuard.Validate(contactDto, _updateValidator);

            var existingContact = await _contactRepository.GetContactByIdAsync(id) ?? throw new NotFoundException("Contact", id);
            var duplicateContact = await _contactRepository.GetByMobilePhoneAsync(contactDto.MobilePhone);
            if (duplicateContact is not null && duplicateContact.Id != id)
            {
                throw new DuplicateContactException(contactDto.MobilePhone);
            }

            _mapper.Map(contactDto, existingContact);
            var updatedContact = await _contactRepository.UpdateContactAsync(existingContact);

            return _mapper.Map<ContactDto>(updatedContact);
        }

        public async Task<ContactDto> DeleteContactAsync(Guid id)
        {
            var existingContact = await _contactRepository.GetContactByIdAsync(id) ?? throw new NotFoundException("Contact", id);
            var deletedContact = await _contactRepository.DeleteContactAsync(id);

            return _mapper.Map<ContactDto>(deletedContact);
        }

    }
}
