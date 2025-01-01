using System.Text.Json;
using Business.Entities;

namespace Business_Tests.Helpers;

public class TestData_Tests
{
    // I ended up wanting to test my own TestData file, in order to assure myself that It actually worked as it should.
    // It's also a learning moment to dump lots of tests in like this, even if it's mostly created from asking ChatGPT.
    
    [Fact]
    public void TestGetSampleContactRegistrationForm()
    {
        var contactForm = TestData.GetSampleContactRegistrationForm();

        Assert.NotNull(contactForm);
        Assert.Equal("Slisk", contactForm.FirstName);
        Assert.Equal("Lindqvist", contactForm.LastName);
        Assert.Equal("slisk.lindqvist@querzion.com", contactForm.Email);
    }

    [Fact]
    public void TestGetSampleContactRegistrationForms()
    {
        var contactForms = TestData.GetSampleContactRegistrationForms(5);

        Assert.NotNull(contactForms);
        Assert.Equal(5, contactForms.Count);
        Assert.Equal("Slisk0", contactForms[0].FirstName);
        Assert.Equal("slisk.lindqvist0@querzion.com", contactForms[0].Email);
        // I added this afterwards.
        Assert.Equal("Slisk1", contactForms[1].FirstName);
        Assert.Equal("slisk.lindqvist1@querzion.com", contactForms[1].Email);
        Assert.Equal("Slisk2", contactForms[2].FirstName);
        Assert.Equal("slisk.lindqvist2@querzion.com", contactForms[2].Email);
        Assert.Equal("Slisk3", contactForms[3].FirstName);
        Assert.Equal("slisk.lindqvist3@querzion.com", contactForms[3].Email);
        Assert.Equal("Slisk4", contactForms[4].FirstName);
        Assert.Equal("slisk.lindqvist4@querzion.com", contactForms[4].Email);
    }

    [Fact]
    public void TestGetSampleContactEntity()
    {
        var contactEntity = TestData.GetSampleContactEntity();

        Assert.NotNull(contactEntity);
        Assert.Equal("1", contactEntity.Id);
        Assert.Equal("Slisk", contactEntity.FirstName);
        Assert.Equal("Lindqvist", contactEntity.LastName);
        Assert.Equal("slisk.lindqvist@querzion.com", contactEntity.Email);
    }

    [Fact]
    public void TestGetSampleContactEntities()
    {
        var contactEntities = TestData.GetSampleContactEntities(3);

        Assert.NotNull(contactEntities);
        Assert.Equal(3, contactEntities.Count);
        // I extended this to be more than one of each. Since ChatGPT only tests the first Sample.
        Assert.Equal("1", contactEntities[0].Id);
        Assert.Equal("2", contactEntities[1].Id);
        Assert.Equal("3", contactEntities[2].Id);
        Assert.Equal("Slisk0", contactEntities[0].FirstName);
        Assert.Equal("Slisk1", contactEntities[1].FirstName);
        Assert.Equal("Slisk2", contactEntities[2].FirstName);
        
    }

    [Fact]
    public void TestGetSampleContactEntityJson()
    {
        var json = TestData.GetSampleContactEntityJson();
        var contactList = JsonSerializer.Deserialize<List<ContactEntity>>(json);

        Assert.NotNull(json);
        Assert.Single(contactList);
        Assert.Equal("1", contactList[0].Id);
    }
}