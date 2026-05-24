using Contacts.Application.Dto;
using Contacts.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("contacts/{id:guid}")]
        public async Task<ActionResult<ContactDto>> GetContactById(Guid id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);

            return Ok(contact);
        }

        [HttpGet("contacts")]
        public async Task<ActionResult<List<ContactDto>>> GetContacts()
        {
            var contacts = await _contactService.GetContactsAsync();

            return Ok(contacts);
        }

        [HttpPost("contacts")]
        public async Task<ActionResult<ContactDto>> CreateContact(CreateContactDto contact)
        {
            var createdContact = await _contactService.CreateContactAsync(contact);

            return Ok(createdContact);
        }

        [HttpPut("contacts/{id:guid}")]
        public async Task<ActionResult<ContactDto>> UpdateContact(Guid id, UpdateContactDto contact)
        {
            var updatedConatact = await _contactService.UpdateContactAsync(id, contact);

            return Ok(updatedConatact);
        }

        [HttpDelete("contacts/{id:guid}")]
        public async Task<ActionResult> DeleteContact(Guid id)
        {
            var deletedContact = await _contactService.DeleteContactAsync(id);

            return Ok(deletedContact);
        }
    }
}
