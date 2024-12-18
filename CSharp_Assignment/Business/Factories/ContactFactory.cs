using System.Diagnostics;
using Business.Entities;
using Business.Helpers;
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
                Id = IdGenerator.GenerateUniqueIdentifier(),
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
                FirstName = contactEntity.FirstName,
                LastName = contactEntity.LastName,
                Email = contactEntity.Email,
                Address = contactEntity.Address,
                PostalCode = contactEntity.PostalCode,
                City = contactEntity.City,
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