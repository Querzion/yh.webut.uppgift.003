using Business.Entities;
using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool CreateContact(ContactRegistrationForm form);
    IEnumerable<Contact> GetContacts();
    bool DeleteContact(string id);
    
    /// <summary>
    /// One of the UpdateContact versions are wrong! While UpdateContactEntity works in the console version,
    /// it's not ideal for the gui version, and I have yet to figure out why it works, and at the same time probably
    /// doesn't. I let them both be in, most of the versions that have been created through the teacher video
    /// materials have User user / Product product, etc. Too much ChatGPT i figure. I asked it where it's best to start
    /// when it comes to updating a contact, new that writes over the old, old that just changes the information
    /// it ended up being a very irritating trip. In the end. . . I had to create two just to see which one is the
    /// better one or functional one. I will probably figure this out with some unit tests and mocks down the road. 
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    bool UpdateContact(Contact contact);
    bool UpdateContactEntity(ContactEntity updatedContact);
    
    bool ClearAllContacts();
}