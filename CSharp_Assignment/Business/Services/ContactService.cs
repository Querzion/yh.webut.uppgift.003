using System.ComponentModel.DataAnnotations;
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
        // instead of ! ChatGPT told me to do this ( ?? new List<ContactEntity>(); ) 
        _contacts = _contactRepository.GetFromFile() ?? new List<ContactEntity>();
        return _contacts.Select(contact => ContactFactory.Create(contact))!;
    }
    
    
    public bool DeleteContact(string id)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == id);
        if (contact != null)
        {
            _contacts.Remove(contact);
            _contactRepository.SaveToFile(_contacts);
            Debug.WriteLine($"Contact with ID {id} deleted.");
            return true;
        }

        Debug.WriteLine($"Contact with ID {id} not found.");
        return false;
    }

    // My amazingly bad memory skills got some help from ChatGPT with the void to bool convertion. 
    public bool ClearContacts()
    {
        if (_contacts.Count == 0)
            return false;

        _contacts.Clear();
        return true;
    }

    public bool UpdateContact(ContactEntity cEntity)
    {
        try
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == cEntity.Id);

            if (contact == null)
            {
                Debug.WriteLine($"Contact with ID {cEntity.Id} not found.");
                return false;
            }

            // Update properties
            contact.FirstName = cEntity.FirstName;
            contact.LastName = cEntity.LastName;
            contact.Email = cEntity.Email;
            contact.Address = cEntity.Address;
            contact.PostalCode = cEntity.PostalCode;
            contact.City = cEntity.City;
            contact.PhoneNumber = cEntity.PhoneNumber;

            _contactRepository.SaveToFile(_contacts);
            Debug.WriteLine($"Contact with ID {cEntity.Id} updated.");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating contact: {ex.Message}");
            return false;
        }
    }
}