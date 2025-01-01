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
        Debug.WriteLine($"Attempting to delete contact with ID: {id}");
        Debug.WriteLine($"Current contacts: {string.Join(", ", _contacts.Select(c => c.Id))}");
        
        var contact = _contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            Debug.WriteLine("Contact not found.");
            return false;
        }

        _contacts.Remove(contact);
        _contactRepository.SaveToFile(_contacts);
        Debug.WriteLine($"Contact with ID {id} deleted.");
        return true;
    }

    // My amazingly bad memory skills got some help from ChatGPT with the void to bool convertion. 
    public bool ClearAllContacts()
    {
        if (_contacts.Count == 0)
            return false;

        _contacts.Clear();
        _contactRepository.SaveToFile(_contacts);
        Debug.WriteLine("All contacts have been cleared.");
        return true;
    }

    public bool UpdateContact(ContactEntity entity)
    {
        try
        {
            // Find the contact to update by its ID
            var contact = _contacts.FirstOrDefault(c => c.Id == entity.Id);

            if (contact == null)
            {
                Debug.WriteLine($"Contact with ID {entity.Id} not found.");
                return false;
            }

            // Update properties
            contact.FirstName = entity.FirstName;
            contact.LastName = entity.LastName;
            contact.Email = entity.Email;
            contact.Address = entity.Address;
            contact.PostalCode = entity.PostalCode;
            contact.City = entity.City;
            contact.PhoneNumber = entity.PhoneNumber;

            _contactRepository.SaveToFile(_contacts);
            Debug.WriteLine($"Contact with ID {entity.Id} updated.");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating contact: {ex.Message}");
            return false;
        }
    }
}