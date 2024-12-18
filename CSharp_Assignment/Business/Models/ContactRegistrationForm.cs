using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ContactRegistrationForm
{
    [Required (ErrorMessage = "First name is required")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
    public string FirstName { get; set; } = null!;
    
    [Required (ErrorMessage = "Last name is required")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
    public string LastName { get; set; } = null!;
    
    [Required (ErrorMessage = "Email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email must be in a valid format like user@mail.com")]
    public string Email { get; set; } = null!;
    
    [Required (ErrorMessage = "Address is required")]
    [MinLength(2, ErrorMessage = "Address must be at least 2 characters")]
    public string Address { get; set; } = null!;
    
    [Required (ErrorMessage = "Postal code is required")]
    [RegularExpression(@"^\d{5,}$", ErrorMessage = "Postal code must be a 5 digit string and contain only numbers")]
    public string PostalCode { get; set; } = null!;
    
    [Required (ErrorMessage = "City name is required")]
    [MinLength(2, ErrorMessage = "City name must be at least 2 characters")]
    public string City { get; set; } = null!;
    
    [RegularExpression(@"^\+?\d+$", ErrorMessage = "Phone number must contain only numbers and may start with a '+' for international format. No spaces are allowed.")]
    public string? PhoneNumber { get; set; }
}