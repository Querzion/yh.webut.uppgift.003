using System.Diagnostics;
using Business.Entities;
using Business.Models;

namespace Business.Factories;

public static class ContactFactory
{
    public static ContactRegistrationForm Create()
    {
        return new ContactRegistrationForm();
    }

    public static ContactEntity? Create(ContactRegistrationForm form)
    {
        try
        {
            return new ContactEntity
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                Address = form.Address,
                PostalCode = form.PostalCode,
                City = form.City,
                PhoneNumber = form.PhoneNumber
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating ContactEntity: {ex.Message}");
            return null;
        }
    }

    public static Contact? Create(ContactEntity contactEntity)
    {
        try
        {
            return new Contact
            {
                Id = contactEntity.Id,
                Name = $"{contactEntity.FirstName} {contactEntity.LastName}",
                Email = contactEntity.Email,
                FullAddress = $"{contactEntity.Address}, {contactEntity.PostalCode}, {contactEntity.City}",
                PhoneNumber = contactEntity.PhoneNumber
            };

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating Contact: {ex.Message}");
            return null;
        }
    }
}