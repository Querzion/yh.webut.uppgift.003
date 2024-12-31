using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool CreateContact(ContactRegistrationForm form);
    IEnumerable<Contact> GetContacts();
    bool DeleteContact(string id);
    // bool UpdateContact(ContactRegistrationForm form);
    bool ClearContacts();
}