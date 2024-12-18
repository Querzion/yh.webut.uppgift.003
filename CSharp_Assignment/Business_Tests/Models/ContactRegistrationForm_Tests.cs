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
    
    // [Fact]
    // public void CreateNewContact_ShouldReturnTrue_WhenContactIsCreated()
}