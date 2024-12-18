namespace Business_Tests.Factories;

public class ContactFactoryTests
{
    // I need to test project in the right order.
    // - Creation of menu/menu attributes,
    // - Creation of form,
    // - Creation of ContactEntity,
    // - Creation of Contact, 
    // - Creation of Unique Id,
    // - Check Repositories, Base & Contact
    // - Creation of Contact.json file, 
    // - Creation of Contact.json content,
    // - Find Contact.json file ability,
    // - Read from Contact.json file ability,
    // - Read Contacts from List,
    // - Ability to update contact info,
    // - Ability to delete contact per Id,
    // - Check if the right contact is being deleted.
    
    // Checked with ChatGPT
    // https://chatgpt.com/share/6762a17a-561c-8005-beed-cf5236eeb50c
    // Missing tests are: 
    // - Validate Input in the form,
    // - Check for edge cases ( duplicate contacts or missing contact data )
    // - Test file integrity ( ensure that contact.json/database isn't corrupted )
    // ensues Adding, Updating & Deleting
    // - Test persistence ( verify that the data is actually being saved )
    // after creation, updates or deletion of a contact, and that changes
    // persist after a restart of the application
    // - Check for Error handling - make sure errors
    // (e.g file not found, invalid json) are properly handled. 
}