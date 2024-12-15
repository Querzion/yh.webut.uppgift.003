using Business.Helpers;

namespace Business.Entities;

public class ContactEntity
{
    public string Id { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Address { get; init; } = null!;
    public string PostalCode { get; init; } = null!;
    public string City { get; init; } = null!;
    public string? PhoneNumber { get; init; }
}