using Contacts.Application.Dto;
using Contacts.Domain.Models;
using Mapster;

namespace Contacts.Application.Mapping
{
    public class MappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Contact, ContactDto>().TwoWays();
            config.NewConfig<Contact, CreateContactDto>().TwoWays();
            config.NewConfig<Contact, UpdateContactDto>().TwoWays();

        }
    }
}
