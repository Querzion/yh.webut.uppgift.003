using Business.Entities;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;
    private List<ContactEntity> _contacts = [];
    

    public bool CreateContact(ContactRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Contact> GetContacts()
    {
        throw new NotImplementedException();
    }
}