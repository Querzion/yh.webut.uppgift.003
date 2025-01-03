using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
    
    // [Required(ErrorMessage = "Postal code is required")]
    // [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "Postal code must be in the format '123 45' or '12345'")]
    // public string PostalCode { get; set; } = null!;
    
    // I wanted it to be a standardized pattern.
    private string _postalCode;

    [Required(ErrorMessage = "Postal code is required")]
    [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "Postal code must be in the format '123 45' or '12345'")]
    public string PostalCode
    {
        get => _postalCode;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                // Remove any spaces and reformat to '123 45'
                _postalCode = Regex.Replace(value, @"(\d{3})\s?(\d{2})", "$1 $2");
            }
        }
    }
    
    [Required (ErrorMessage = "City name is required")]
    [MinLength(2, ErrorMessage = "City name must be at least 2 characters")]
    public string City { get; set; } = null!;
    
    [RegularExpression(@"^\+?\d+$", ErrorMessage = "Phone number must contain only numbers and may start with a '+' for international format. No spaces are allowed.")]
    public string? PhoneNumber { get; set; }
}