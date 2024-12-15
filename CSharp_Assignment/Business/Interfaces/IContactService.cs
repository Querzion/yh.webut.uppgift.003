using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool CreateContact(ContactRegistrationForm form);
    IEnumerable<Contact> GetContacts();
}