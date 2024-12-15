using System.Diagnostics;
using Business.Entities;
using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;
    private List<ContactEntity> _contacts = [];
    

    public bool CreateContact(ContactRegistrationForm form)
    {
        try
        {
            var cEntity = ContactFactory.Create(form);

            if (cEntity != null)
            {
                _contacts.Add(cEntity);
                _contactRepository.SaveToFile(_contacts);
                
                return true;
            }
            
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public IEnumerable<Contact> GetContacts()
    {
        _contacts = _contactRepository.GetFromFile()!;
        return _contacts.Select(contact => ContactFactory.Create(contact))!;
    }
}