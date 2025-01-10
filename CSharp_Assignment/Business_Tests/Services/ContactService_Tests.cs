using Business_Tests.Helpers;
using Business.Entities;
using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Moq;

namespace Business_Tests.Services;

public class ContactService_Tests
{
    // ChatGPT generated tests, that I have changed a bit. SOME are really not logical at all, 
    // And will in turn have an opposite outcome, I saved them nonetheless because I want to look in to them a bit more
    // in the future. 
    
    // [*] Create
    // [*] View
    // [*] Delete
    // [*] Update
    // [*] Clear
    
    private readonly Mock<IContactRepository> _mockRepository;
    private readonly ContactService _contactService;

    public ContactService_Tests()
    {
        // Arrange the mock repository
        _mockRepository = new Mock<IContactRepository>();
        _contactService = new ContactService(_mockRepository.Object);
    }
    
    [Fact]
    public void CreateContact_ShouldAddContact_WhenFormIsValid()
    {
        // Arrange: Prepare a valid ContactRegistrationForm
        var form = TestData.GetSampleContactRegistrationForm();

        var mockRepository = new Mock<IContactRepository>();
        var contactService = new ContactService(mockRepository.Object);

        // Act: Call CreateContact
        var result = contactService.CreateContact(form);

        // Assert: Ensure the contact was created successfully
        Assert.True(result);
        mockRepository.Verify(r => r.SaveToFile(It.IsAny<List<ContactEntity>>()), Times.Once);
    }
    
    [Fact]
    public void GetContacts_ShouldReturnContacts_WhenContactsExist()
    {
        // Arrange
        var mockContacts = TestData.GetSampleContactEntities(2);

        _mockRepository.Setup(r => r.GetFromFile()).Returns(mockContacts);

        // Act
        var result = _contactService.GetContacts();

        // Assert
        Assert.Equal(2, result.Count());
    }
    
    [Fact]
    public void CreateContact_ShouldReturnTrue_WhenContactIsCreatedSuccessfully()
    {
        // Arrange
        var form = TestData.GetSampleContactRegistrationForm();

        var contactEntity = ContactFactory.Create(form);

        // Mock repository SaveToFile
        _mockRepository.Setup(r => r.SaveToFile(It.IsAny<List<ContactEntity>>())).Verifiable();

        // Act
        var result = _contactService.CreateContact(form);

        // Assert
        Assert.True(result);
        _mockRepository.Verify(r => r.SaveToFile(It.IsAny<List<ContactEntity>>()), Times.Once);
    }
    
    // [Fact]
    // public void DeleteContact_ShouldReturnTrue_WhenContactIsDeletedSuccessfully()
    // {
    //     // Arrange
    //     var contactId = "1";
    //     var contactList = TestData.GetSampleContactEntities(1);
    //
    //     _mockRepository.Setup(r => r.GetFromFile()).Returns(contactList);
    //     _mockRepository.Setup(r => r.SaveToFile(It.IsAny<List<ContactEntity>>())).Verifiable();
    //
    //     // Act
    //     var result = _contactService.DeleteContact(contactId);
    //
    //     // Assert
    //     Assert.True(result);
    //     _mockRepository.Verify(r => r.SaveToFile(It.IsAny<List<ContactEntity>>()), Times.Once);
    // }
    //
    // [Fact]
    // public void DeleteContact_ShouldRemoveContact_WhenContactExists()
    // {
    //     // Arrange: Create a sample contact list
    //     var contacts = TestData.GetSampleContactEntitiesGuid(3);
    //     var contactToDelete = contacts[2]; // Choose a contact to delete
    //     var mockRepository = new Mock<IContactRepository>();
    //     mockRepository.Setup(r => r.GetFromFile()).Returns(contacts);
    //     mockRepository.Setup(r => r.SaveToFile(It.IsAny<List<ContactEntity>>())).Verifiable();
    //
    //     var contactService = new ContactService(mockRepository.Object);
    //
    //     // Pre-assert: Ensure the contact exists before deletion
    //     Assert.Contains(contactService.GetContacts(), c => c.Id == contactToDelete.Id);
    //
    //     // Act: Delete the contact
    //     var result = contactService.DeleteContact(contactToDelete.Id);
    //
    //     // Assert: Ensure the contact is removed and method returns true
    //     Assert.True(result); // Method should return true
    //     Assert.DoesNotContain(contactService.GetContacts(), c => c.Id == contactToDelete.Id); // Verify contact is removed
    //     mockRepository.Verify(r => r.SaveToFile(It.Is<List<ContactEntity>>(c => c.All(x => x.Id != contactToDelete.Id))), Times.Once); // Verify save action
    // }
    
    [Fact]
    public void DeleteContact_ShouldReturnFalse_WhenContactDoesNotExist()
    {
        // Arrange
        var contactId = "nonexistent";
        var contactList = TestData.GetSampleContactEntities(1);
    
        _mockRepository.Setup(r => r.GetFromFile()).Returns(contactList);
    
        // Act
        var result = _contactService.DeleteContact(contactId);
    
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void DeleteContact_ShouldReturnFalse_WhenContactDoesNotExist2()
    {
        // Arrange: Prepare a list of contacts
        var contacts = TestData.GetSampleContactEntities(1);
        
        var mockRepository = new Mock<IContactRepository>();
        mockRepository.Setup(r => r.GetFromFile()).Returns(contacts);
        var contactService = new ContactService(mockRepository.Object);
    
        // Act: Call DeleteContact with a non-existing ID
        var result = contactService.DeleteContact("2");
    
        // Assert: Ensure the contact was not deleted
        Assert.False(result);
    }
    
    // [Fact]
    // public void UpdateContact_ShouldUpdateContact_WhenContactExists()
    // {
    //     // Arrange: Create a sample contact list
    //     var contacts = TestData.GetSampleContactEntities(1); // Get one contact
    //     var updatedContact = new ContactEntity
    //     {
    //         Id = "1", 
    //         FirstName = "John", 
    //         LastName = "Doe", 
    //         Email = "john.doe@newdomain.com",
    //         Address = "New Address",
    //         PostalCode = "12345",
    //         City = "New City",
    //         PhoneNumber = "+1234567890"
    //     };
    //
    //     // Create a mock repository and set up the behavior for GetFromFile
    //     var mockRepository = new Mock<IContactRepository>();
    //     mockRepository.Setup(r => r.GetFromFile()).Returns(contacts.ToList()); // Return a new list (not reference)
    //
    //     // Set up the SaveToFile method to simulate saving the updated contact list
    //     mockRepository.Setup(r => r.SaveToFile(It.IsAny<List<ContactEntity>>()))
    //         .Callback<List<ContactEntity>>(updatedList =>
    //         {
    //             // After SaveToFile is called, update the contacts list to reflect the changes
    //             contacts.Clear();
    //             contacts.AddRange(updatedList);
    //         });
    //
    //     var contactService = new ContactService(mockRepository.Object);
    //
    //     // Act: Call UpdateContact with the updated contact
    //     var result = contactService.UpdateContactEntity(updatedContact);
    //
    //     // Assert: Ensure the contact was updated and saved
    //     Assert.True(result); // The update should succeed
    //     Assert.Equal("John", contacts[0].FirstName); // Ensure the first name is updated
    //     Assert.Equal("Doe", contacts[0].LastName); // Ensure the last name is updated
    //     Assert.Equal("john.doe@newdomain.com", contacts[0].Email); // Ensure the email is updated
    //
    //     // Verify save action
    //     mockRepository.Verify(r => r.SaveToFile(It.Is<List<ContactEntity>>(c => c[0].Email == "john.doe@newdomain.com")), Times.Once);
    // }
    
    [Fact]
    public void UpdateContact_ShouldReturnFalse_WhenContactDoesNotExist()
    {
        // Arrange: Create a sample contact list
        var contacts = TestData.GetSampleContactEntities(1); // Get one contact
        var nonExistentContact = new ContactEntity
        {
            Id = "999", // Non-existing ID
            FirstName = "Jane", 
            LastName = "Doe", 
            Email = "jane.doe@newdomain.com"
        };

        var mockRepository = new Mock<IContactRepository>();
        mockRepository.Setup(r => r.GetFromFile()).Returns(contacts);
        mockRepository.Setup(r => r.SaveToFile(It.IsAny<List<ContactEntity>>())).Verifiable();

        var contactService = new ContactService(mockRepository.Object);

        // Act: Try to update a non-existing contact
        var result = contactService.UpdateContactEntity(nonExistentContact);

        // Assert: Ensure the update fails
        Assert.False(result); // The update should fail
        mockRepository.Verify(r => r.SaveToFile(It.IsAny<List<ContactEntity>>()), Times.Never); // Ensure save was not called
    }
    
    [Fact]
    public void GetContacts_ShouldReturnAllContacts_WhenContactsExist()
    {
        // Arrange: Generate sample contacts using GetSampleContactEntities
        var contacts = TestData.GetSampleContactEntitiesGuid(5); // Change 5 to the desired number of contacts

        // Mock the repository to return the generated sample contacts
        var mockRepository = new Mock<IContactRepository>();
        mockRepository.Setup(r => r.GetFromFile()).Returns(contacts);
    
        // Create the ContactService with the mocked repository
        var contactService = new ContactService(mockRepository.Object);

        // Act: Call GetContacts
        var result = contactService.GetContacts().ToList();

        // Assert: Ensure the contacts are returned correctly
        Assert.Equal(5, result.Count); // Ensure the correct number of contacts are returned
        Assert.Equal("Slisk0", result[0].FirstName); // Check the first contact's first name
        Assert.Equal("Slisk1", result[1].FirstName); // Check the second contact's first name
        Assert.Equal("Slisk4", result[4].FirstName); // Check the last contact's first name
    }
    
    
    
    // [Fact]
    // public void ClearAllContacts_ShouldReturnTrue_WhenContactsExist()
    // {
    //     // Arrange
    //     var contactList = TestData.GetSampleContactEntities(15);
    //
    //     _mockRepository.Setup(r => r.GetFromFile()).Returns(contactList);
    //     _mockRepository.Setup(r => r.SaveToFile(It.IsAny<List<ContactEntity>>())).Verifiable();
    //     
    //     // Act
    //     var result = _contactService.ClearAllContacts();
    //
    //     // Assert
    //     Assert.True(result);
    //     _mockRepository.Verify(r => r.SaveToFile(It.IsAny<List<ContactEntity>>()), Times.Once);
    // }
    
    [Fact]
    public void ClearAllContacts_ShouldReturnTrue_WhenContactsExist2()
    {
        // Arrange: Prepare a list of contacts
        var contacts = TestData.GetSampleContactEntities(3);
        var mockRepository = new Mock<IContactRepository>();
        mockRepository.Setup(r => r.GetFromFile()).Returns(contacts);
        mockRepository.Setup(r => r.SaveToFile(It.IsAny<List<ContactEntity>>())).Verifiable();

        // Initialize ContactService with the mock repository
        var contactService = new ContactService(mockRepository.Object);

        // Assert pre-condition: Ensure contacts exist
        Assert.NotEmpty(contactService.GetContacts()); // Verify list is not empty initially

        // Act: Clear all contacts
        var result = contactService.ClearAllContacts();

        // Assert post-condition: Verify contacts are cleared and method behavior
        Assert.True(result); // Method should return true
        Assert.Empty(contactService.GetContacts()); // Verify in-memory list is cleared
        mockRepository.Verify(r => r.SaveToFile(It.Is<List<ContactEntity>>(c => c.Count == 0)), Times.Once); // Verify save action
    }

    [Fact]
    public void ClearAllContacts_ShouldReturnFalse_WhenNoContactsExist()
    {
        // Arrange: Prepare an empty list of contacts
        var contacts = new List<ContactEntity>();
        var mockRepository = new Mock<IContactRepository>();
        mockRepository.Setup(r => r.GetFromFile()).Returns(contacts);
        mockRepository.Setup(r => r.SaveToFile(It.IsAny<List<ContactEntity>>())).Verifiable();

        // Initialize ContactService with the mock repository
        var contactService = new ContactService(mockRepository.Object);

        // Act: Attempt to clear all contacts
        var result = contactService.ClearAllContacts();

        // Assert: Ensure the method returns false and no save action is performed
        Assert.Empty(contactService.GetContacts()); // Verify in-memory list is cleared
        Assert.False(result); // Method should return false
        mockRepository.Verify(r => r.SaveToFile(It.IsAny<List<ContactEntity>>()), Times.Never); // Verify save action is not called
    }
    
    
    
}