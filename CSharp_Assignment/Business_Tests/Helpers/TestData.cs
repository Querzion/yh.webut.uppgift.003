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
}