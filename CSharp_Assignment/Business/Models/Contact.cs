using Business.Helpers;

namespace Business.Models;

public class Contact
{
    public string Id { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string FullAddress { get; init; } = null!;
    public string? PhoneNumber { get; init; }
}