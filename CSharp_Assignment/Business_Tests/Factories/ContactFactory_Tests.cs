using Business_Tests.Helpers;
using Business.Entities;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Moq;

namespace Business_Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnContactRegistrationForm()
    {
        ContactRegistrationForm result = ContactFactory.Create();
        
        Assert.NotNull(result);
        Assert.IsType<ContactRegistrationForm>(result);
    }
    
    [Fact]
    public void Create_ShouldReturnContactRegistrationForm2()
    {
        var result = ContactFactory.Create();
        
        Assert.NotNull(result);
        Assert.IsType<ContactRegistrationForm>(result);
    }
    
    [Fact]
    public void Create_ShouldReturnContactEntity_WhenContactRegistrationFormIsProvided()
    {
        // Arrange: Set up a ContactRegistrationForm with some data
        var form = TestData.GetSampleContactRegistrationForm();

        // Act: Create a ContactEntity from the form
        var contactEntity = ContactFactory.Create(form);

        Assert.NotNull(contactEntity);  // Ensure the ContactEntity is created
        Assert.IsType<ContactEntity>(contactEntity);
    }
    
    [Fact]
    public void Create_ShouldReturnContact_WhenContactEntityIsProvided()
    {
        // Arrange: Set up a ContactRegistrationForm with some data
        var contactEntity = TestData.GetSampleContactEntity();

        // Act: Create a ContactEntity from the form
        var contact = ContactFactory.Create(contactEntity);

        Assert.NotNull(contact);  // Ensure the ContactEntity is created
        Assert.IsType<Contact>(contact);
    }

    
    // Test Created by ChatGPT, Changed info.
    [Fact]
    public void ContactFactory_CreateContactEntity_FromForm()
    {
        // Arrange: Set up a ContactRegistrationForm with some data
        var form = TestData.GetSampleContactRegistrationForm();

        // Act: Create a ContactEntity from the form
        var contactEntity = ContactFactory.Create(form);

        // Assert: Check if the contactEntity is not null
        Assert.NotNull(contactEntity);

        // Assert: Check if the contactEntity has the correct values
        Assert.Equal(form.FirstName, contactEntity.FirstName);
        Assert.Equal(form.LastName, contactEntity.LastName);
        Assert.Equal(form.Email, contactEntity.Email);
        Assert.Equal(form.Address, contactEntity.Address);
        Assert.Equal(form.PostalCode, contactEntity.PostalCode);
        Assert.Equal(form.City, contactEntity.City);
        Assert.Equal(form.PhoneNumber, contactEntity.PhoneNumber);

        // Assert: Check that Id is generated and not null (assuming IdGenerator generates a valid ID)
        Assert.False(string.IsNullOrEmpty(contactEntity.Id));
    }
    
    [Fact]
    public void ContactFactory_CreateContact_FromEntity()
    {
        // Arrange: Set up a ContactEntity with some data
        var contactEntity = TestData.GetSampleContactEntity();

        // Act: Create a Contact from the ContactEntity
        var contact = ContactFactory.Create(contactEntity);

        // Assert: Check if the contact is not null
        Assert.NotNull(contact);

        // Assert: Check if the contact has the correct values
        Assert.Equal(contactEntity.Id, contact.Id);
        Assert.Equal(contactEntity.FirstName, contact.FirstName);
        Assert.Equal(contactEntity.LastName, contact.LastName);
        Assert.Equal(contactEntity.Email, contact.Email);
        Assert.Equal(contactEntity.Address, contact.Address);
        Assert.Equal(contactEntity.PostalCode, contact.PostalCode);
        Assert.Equal(contactEntity.City, contact.City);
        Assert.Equal(contactEntity.PhoneNumber, contact.PhoneNumber);
    }

    
}