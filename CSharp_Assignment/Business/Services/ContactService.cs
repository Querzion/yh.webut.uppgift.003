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
            return true;
        }
        return false;
    }
    
    // public bool UpdateContact(ContactEntity cEntity)
    // {
    //     try
    //     {
    //         // Find the contact to update
    //         var contact = _contacts.FirstOrDefault(c => c.Id == cEntity.Id);
    //
    //         if (contact == null)
    //         {
    //             return false; // Contact not found
    //         }
    //
    //         // Update the contact's properties
    //         contact.FirstName = cEntity.FirstName;
    //         contact.LastName = cEntity.LastName;
    //         contact.Email = cEntity.Email;
    //         contact.Address = cEntity.Address;
    //         contact.PostalCode = cEntity.PostalCode;
    //         contact.City = cEntity.City;
    //         contact.PhoneNumber = cEntity.PhoneNumber;
    //
    //         // Save the updated contact list
    //         _contactRepository.SaveToFile(_contacts);
    //
    //         return true;
    //     }
    //     catch (Exception ex)
    //     {
    //         // Log the exception (optional)
    //         Debug.WriteLine($"An error occurred while updating the contact: {ex.Message}");
    //         return false; // Return false in case of an error
    //     }
    // }
}