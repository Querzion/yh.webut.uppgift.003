using System.Text.Json;
using Business.Entities;
using Business.Models;

namespace Business_Tests.Helpers;

public static class TestData
{
    // It was too timeconsuming writing this down over and over again.
    // It's probably going to be in use more often then the three times I have
    // currently used them.
    public static ContactRegistrationForm GetSampleContactRegistrationForm()
    {
        return new ContactRegistrationForm
        {
            FirstName = "Slisk",
            LastName = "Lindqvist",
            Email = "slisk.lindqvist@querzion.com",
            Address = "Västgötagatan 10j",
            PostalCode = "681 40",
            City = "Kristinehamn",
            PhoneNumber = "+46700797082"
        };
    }

    // Multiple samples
    public static List<ContactRegistrationForm> GetSampleContactRegistrationForms(int count)
    {
        var samples = new List<ContactRegistrationForm>();
        for (int i = 0; i < count; i++)
        {
            samples.Add(new ContactRegistrationForm
            {
                FirstName = $"Slisk{i}",
                LastName = "Lindqvist",
                Email = $"slisk.lindqvist{i}@querzion.com",
                Address = "Västgötagatan 10j",
                PostalCode = "681 40",
                City = "Kristinehamn",
                PhoneNumber = "+46700797082"
            });
        }
        return samples;
    }

    // Single sample
    public static ContactEntity GetSampleContactEntity()
    {
        return new ContactEntity
        {
            Id = "1",
            FirstName = "Slisk",
            LastName = "Lindqvist",
            Email = "slisk.lindqvist@querzion.com",
            Address = "Västgötagatan 10j",
            PostalCode = "681 40",
            City = "Kristinehamn",
            PhoneNumber = "+46700797082"
        };
    }

    // Multiple samples
    public static List<ContactEntity> GetSampleContactEntities(int count)
    {
        var samples = new List<ContactEntity>();
        for (int i = 0; i < count; i++)
        {
            samples.Add(new ContactEntity
            {
                Id = (i + 1).ToString(),
                FirstName = $"Slisk{i}",
                LastName = "Lindqvist",
                Email = $"slisk.lindqvist{i}@querzion.com",
                Address = "Västgötagatan 10j",
                PostalCode = "681 40",
                City = "Kristinehamn",
                PhoneNumber = "+46700797082"
            });
        }
        return samples;
    }
    public static List<ContactEntity> GetSampleContactEntitiesGuid(int count)
    {
        var samples = new List<ContactEntity>();
        for (int i = 0; i < count; i++)
        {
            samples.Add(new ContactEntity
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = $"Slisk{i}",
                LastName = "Lindqvist",
                Email = $"slisk.lindqvist{i}@querzion.com",
                Address = "Västgötagatan 10j",
                PostalCode = "681 40",
                City = "Kristinehamn",
                PhoneNumber = "+46700797082"
            });
        }
        return samples;
    }
    
    // Returns a JSON string representation of a sample ContactEntity
    public static string GetSampleContactEntityJson()
    {
        var contact = GetSampleContactEntity();
        var list = new List<ContactEntity> { contact };
        return JsonSerializer.Serialize(list);
    }
}