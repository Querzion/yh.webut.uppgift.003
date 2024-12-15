using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ContactRegistrationForm
{
    [Required (ErrorMessage = "First name is required")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
    public string FirstName { get; init; } = null!;
    
    [Required (ErrorMessage = "Last name is required")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
    public string LastName { get; init; } = null!;
    
    [Required (ErrorMessage = "Email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email must be in a valid format like user@mail.com")]
    public string Email { get; init; } = null!;
    
    [Required (ErrorMessage = "Address is required")]
    [MinLength(2, ErrorMessage = "Address must be at least 2 characters")]
    public string Address { get; init; } = null!;
    
    [Required (ErrorMessage = "Postal code is required")]
    [MinLength(5, ErrorMessage = "Postal code must be at least 5 characters")]
    public string PostalCode { get; init; } = null!;
    
    [Required (ErrorMessage = "City name is required")]
    [MinLength(2, ErrorMessage = "City name must be at least 2 characters")]
    public string City { get; init; } = null!;
    
    public string? PhoneNumber { get; init; }
}