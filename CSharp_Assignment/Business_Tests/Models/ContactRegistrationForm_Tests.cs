using System.ComponentModel.DataAnnotations;
using Business_Tests.Helpers;
using Business.Factories;
using Business.Models;

namespace Business_Tests.Models;

public class ContactRegistrationForm_Tests
{
    [Fact]
    public void Create_ShouldReturnContactRegistrationForm()
    {
        ContactRegistrationForm result = ContactFactory.Create();
        
        Assert.IsType<ContactRegistrationForm>(result);
    }
    
    // ChatGPT generated tests in order to test validation process.
    
    private static ValidationContext GetValidationContext(object model) =>
        new ValidationContext(model, null, null);

    private static IList<ValidationResult> ValidateModel(object model)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, GetValidationContext(model), results, true);
        return results;
    }

    [Fact]
    public void FirstName_WhenEmpty_ShouldFailValidation()
    {
        // Arrange
        var form = new ContactRegistrationForm { FirstName = "", LastName = "Doe", Email = "test@mail.com", Address = "123 Main St", PostalCode = "12345", City = "City" };

        // Act
        var results = ValidateModel(form);

        // Assert
        Assert.Contains(results, r => r.ErrorMessage == "First name is required");
    }

    [Fact]
    public void FirstName_WhenTooShort_ShouldFailValidation()
    {
        // Arrange
        var form = new ContactRegistrationForm { FirstName = "A", LastName = "Doe", Email = "test@mail.com", Address = "123 Main St", PostalCode = "12345", City = "City" };

        // Act
        var results = ValidateModel(form);

        // Assert
        Assert.Contains(results, r => r.ErrorMessage == "First name must be at least 2 characters");
    }

    [Fact]
    public void Email_WhenInvalid_ShouldFailValidation()
    {
        // Arrange
        var form = new ContactRegistrationForm { FirstName = "John", LastName = "Doe", Email = "invalid-email", Address = "123 Main St", PostalCode = "12345", City = "City" };

        // Act
        var results = ValidateModel(form);

        // Assert
        Assert.Contains(results, r => r.ErrorMessage == "Email must be in a valid format like user@mail.com");
    }

    [Fact]
    public void PostalCode_WhenValid_ShouldReformatCorrectly()
    {
        // Arrange
        var form = new ContactRegistrationForm { FirstName = "John", LastName = "Doe", Email = "test@mail.com", Address = "123 Main St", PostalCode = "12345", City = "City" };

        // Act
        var results = ValidateModel(form);

        // Assert
        Assert.Empty(results); // No validation errors
        Assert.Equal("123 45", form.PostalCode); // Postal code is reformatted
    }

    [Fact]
    public void PostalCode_WhenInvalid_ShouldFailValidation()
    {
        // Arrange
        var form = new ContactRegistrationForm { FirstName = "John", LastName = "Doe", Email = "test@mail.com", Address = "123 Main St", PostalCode = "invalid", City = "City" };

        // Act
        var results = ValidateModel(form);

        // Assert
        Assert.Contains(results, r => r.ErrorMessage == "Postal code must be in the format '123 45' or '12345'");
    }

    [Fact]
    public void PhoneNumber_WhenInvalid_ShouldFailValidation()
    {
        // Arrange
        var form = new ContactRegistrationForm { FirstName = "John", LastName = "Doe", Email = "test@mail.com", Address = "123 Main St", PostalCode = "12345", City = "City", PhoneNumber = "123 456" };

        // Act
        var results = ValidateModel(form);

        // Assert
        Assert.Contains(results, r => r.ErrorMessage == "Phone number must contain only numbers and may start with a '+' for international format. No spaces are allowed.");
    }

    [Fact]
    public void AllFields_WhenValid_ShouldPassValidation()
    {
        // Arrange
        var form = TestData.GetSampleContactRegistrationForm();

        // Act
        var results = ValidateModel(form);

        // Assert
        Assert.Empty(results); // No validation errors
    }
}