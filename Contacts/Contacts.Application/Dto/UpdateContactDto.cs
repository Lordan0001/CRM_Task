namespace Contacts.Application.Dto
{
    public class UpdateContactDto
    {
        public string Name { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}
