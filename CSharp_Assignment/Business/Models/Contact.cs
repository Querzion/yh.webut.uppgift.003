using Business.Helpers;

namespace Business.Models;

public class Contact
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    
    public string FullName { get; set; } = null!;
    public string FullAddress { get; set; } = null!;
    public string FullContact { get; set; } = null!;
}