using System.ComponentModel.DataAnnotations;
using Business.Entities;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using MA_Presentation_Console.Interfaces;
using static System.Console;

namespace MA_Presentation_Console.Dialogs;

public class MenuDialog(IContactService contactService) : IMenuDialog
{
    private readonly IContactService _contactService = contactService;
    private bool isRunning = true;
    

    public void ShowMainMenu()
    {
        do 
        {
            MainDialogOptions();
        } while (isRunning);
    }

    public void MainDialogOptions()
    {
        Dialogs.MenuHeading("Main Menu");
        WriteLine("1. View Contacts");
        WriteLine("2. Create Contact");
        WriteLine("3. Update Contact");
        WriteLine("4. Delete Contact");
        WriteLine("5. Clear Contacts");
        WriteLine("Q. Exit Application");
        WriteLine("_____________________________________________");
        var option = ReadLine()!;

        switch (option.ToLower())
        {
            case "1":
                Dialogs.MenuHeading("View Contacts");
                ViewContactsOption();
                ReadKey();
                break;
            case "2":
                Dialogs.MenuHeading("Create Contact");
                CreateContactOption();
                // ReadKey();
                break;
            case "3":
                Dialogs.MenuHeading("Update Contact");
                UpdateContactOption();
                ReadKey();
                break;
            case "4":
                Dialogs.MenuHeading("Delete Contact");
                DeleteContactOption();
                ReadKey();
                break;
            case "5":
                Dialogs.MenuHeading("Clear Contacts");
                ClearContactsOption();
                ReadKey();
                break;
            case "q":
                Dialogs.MenuHeading("Exit Application");
                WriteLine("Program exit... Press any key to exit...");
                ReadKey();
                isRunning = false;
                break;
            default:
                WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private void ClearContactsOption()
    {
        Dialogs.MenuHeading("Clear Contacts");
        WriteLine("Are you sure you want to delete all contacts? This action cannot be undone. (yes/no)");

        var input = ReadLine()?.Trim().ToLower();
        if (input == "yes")
        {
            // Clear all contacts
            _contactService.ClearAllContacts();
            WriteLine("All contacts have been deleted.");
        }
        else if (input == "no")
        {
            WriteLine("Operation canceled. No contacts were deleted.");
        }
        else
        {
            WriteLine("Invalid input. Operation canceled.");
        }
    }

    public void ViewContactsOption()
    {
        try
        {
            // Get the contacts from the file
            var contacts = _contactService.GetContacts();
        
            // Check if the list is null or empty
            if (contacts == null || contacts.Count() == 0)
            {
                WriteLine("No contacts found.");
                return;
            }

            // Display the contacts
            WriteLine("Contacts List:");
            foreach (var contact in contacts)
            {
                // Display the contact details (adjust the fields as needed)
                WriteLine($"\nID: {contact.Id}");
                WriteLine(new string('-', 50)); // Divider for readability
                WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                WriteLine($"Email: {contact.Email}");
                WriteLine($"Address: {contact.Address}, {contact.PostalCode} {contact.City}");
                WriteLine($"Phone: {contact.PhoneNumber ?? "N/A"}");
                WriteLine(new string('-', 50)); // Divider for readability
            }
        }
        catch (Exception ex)
        {
            // Log the error and show a friendly message
            WriteLine("An error occurred while loading the contacts.");
            WriteLine($"Error: {ex.Message}");
        }
    }
    
    public void CreateContactOption()
    {
        Dialogs.MenuHeading("New Contact");

        // Create an instance of the form
        // var form = new ContactRegistrationForm();
        var form = ContactFactory.Create();
        
        // Use PromptAndValidate to collect and validate input for each property
        form.FirstName = PromptAndValidate("Enter First Name: ", nameof(ContactRegistrationForm.FirstName), form);
        form.LastName = PromptAndValidate("Enter Last Name: ", nameof(ContactRegistrationForm.LastName), form);
        form.Email = PromptAndValidate("Enter Email: ", nameof(ContactRegistrationForm.Email), form);
        form.Address = PromptAndValidate("Enter Address: ", nameof(ContactRegistrationForm.Address), form);
        form.PostalCode = PromptAndValidate("Enter Postal Code: ", nameof(ContactRegistrationForm.PostalCode), form);
        form.City = PromptAndValidate("Enter City: ", nameof(ContactRegistrationForm.City), form);
        // Not required, so no validation is added to.
        form.PhoneNumber = Dialogs.Prompt("Enter Phone Number (optional): ");

        // Pass the validated form to the service
        var result = _contactService.CreateContact(form);

        if (result)
        {
            WriteLine();
            WriteLine("New Contact successfully created.");
        }
        else
        {
            WriteLine();
            WriteLine("New Contact failed to be created. Please try again.");
        }

        ReadKey();
    }
    
    private void DeleteContactOption()
    {
        Dialogs.MenuHeading("Delete Contact");
        
        var contacts = _contactService.GetContacts().ToList();

        if (!contacts.Any())
        {
            WriteLine("No contacts available for deletion.");
            return;
        }

        // Display contacts with index numbers
        WriteLine("Here are the contacts available for deletion:");
        for (int i = 0; i < contacts.Count; i++)
        {
            WriteLine($"{i + 1}: {contacts[i].FirstName} {contacts[i].LastName} (ID: {contacts[i].Id})");
        }

        WriteLine("Enter the number of the contact you want to delete:");
        if (int.TryParse(ReadLine(), out int index) && index > 0 && index <= contacts.Count)
        {
            var contactToDelete = contacts[index - 1];
            if (_contactService.DeleteContact(contactToDelete.Id))
            {
                WriteLine($"Contact {contactToDelete.FirstName} {contactToDelete.LastName} has been deleted.");
            }
            else
            {
                WriteLine("Failed to delete the contact. Please try again.");
            }
        }
        else
        {
            WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    public void UpdateContactOption()
    {
        Dialogs.MenuHeading("Update Contact");

        // Retrieve the contacts list from the service
        var contacts = _contactService.GetContacts().ToList();

        if (!contacts.Any())
        {
            WriteLine("No contacts available for update.");
            return;
        }

        // Display contacts for selection
        WriteLine("Here are the contacts available for update:");
        for (int i = 0; i < contacts.Count; i++)
        {
            WriteLine($"\nContact Nr: {i + 1} with (ID: {contacts[i].Id})");
            WriteLine($"{contacts[i].FirstName} {contacts[i].LastName} ");
            WriteLine($"{contacts[i].Email}");
            WriteLine($"{contacts[i].Address}");
            WriteLine($"{contacts[i].PostalCode} {contacts[i].City}");
            WriteLine($"{contacts[i].PhoneNumber}");
            WriteLine(new string('-', 50)); // Divider for readability
        }

        WriteLine("Enter the number of the contact you want to update:");
        if (int.TryParse(ReadLine(), out int index) && index > 0 && index <= contacts.Count)
        {
            var contactToUpdate = contacts[index - 1];
            WriteLine($"You have selected {contactToUpdate.FirstName} {contactToUpdate.LastName} (ID: {contactToUpdate.Id})");

            // Convert Business.Models.Contact to Business.Entities.ContactEntity
            var contactEntity = ConvertToContactEntity(contactToUpdate);

            // Now, get the updated contact information from the user
            var updatedForm = GetContactFormFromUser(contactEntity); // Prefill the form with existing data

            // Update contact properties from the form
            contactEntity.FirstName = PromptAndValidate("Enter First Name", "FirstName", updatedForm);
            contactEntity.LastName = PromptAndValidate("Enter Last Name", "LastName", updatedForm);
            contactEntity.Email = PromptAndValidate("Enter Email", "Email", updatedForm);
            contactEntity.Address = PromptAndValidate("Enter Address", "Address", updatedForm);
            contactEntity.PostalCode = PromptAndValidate("Enter Postal Code", "PostalCode", updatedForm);
            contactEntity.City = PromptAndValidate("Enter City", "City", updatedForm);
            contactEntity.PhoneNumber = PromptAndValidate("Enter Phone Number", "PhoneNumber", updatedForm);

            // Use the service to update the contact
            bool updated = _contactService.UpdateContact(contactEntity); // Call update method

            if (updated)
            {
                Clear();
                Dialogs.MenuHeading("CONTACT UPDATED");
                WriteLine($"\nContact {contactEntity.FirstName} {contactEntity.LastName} has been updated successfully.");
            }
            else
            {
                Clear();
                Dialogs.MenuHeading("FAILED TO UPDATE");
                WriteLine("\nFailed to update the contact. Please try again.");
            }
        }
        else
        {
            WriteLine("\nInvalid input. Please enter a valid number.");
        }
    }
    
    // Used for CreateContactOption only
    private string PromptAndValidate(string prompt, string propertyName, ContactRegistrationForm crf)
    {
        while (true)
        {
            WriteLine();
            WriteLine(prompt);
            var input = ReadLine() ?? string.Empty;
    
            // Create a ValidationContext for the specific property
            var results = new List<ValidationResult>();
            var context = new ValidationContext(crf) { MemberName = propertyName };
    
            // Temporarily set the property value
            typeof(ContactRegistrationForm)
                .GetProperty(propertyName)!
                .SetValue(crf, input);
    
            // Validate the property
            if (Validator.TryValidateProperty(input, context, results))
                return input;
    
            // Display the validation error
            WriteLine($"{results[0].ErrorMessage}. Please try again.");
        }
    }
    
    // Convert Business.Models.Contact to Business.Entities.ContactEntity
    private ContactEntity ConvertToContactEntity(Contact contact)
    {
        return new ContactEntity
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
            Address = contact.Address,
            PostalCode = contact.PostalCode,
            City = contact.City,
            PhoneNumber = contact.PhoneNumber
        };
    }
    
    private ContactRegistrationForm GetContactFormFromUser(ContactEntity contactEntity)
    {
        return new ContactRegistrationForm
        {
            FirstName = contactEntity.FirstName,
            LastName = contactEntity.LastName,
            Email = contactEntity.Email,
            Address = contactEntity.Address,
            PostalCode = contactEntity.PostalCode,
            City = contactEntity.City,
            PhoneNumber = contactEntity.PhoneNumber
        };
    }
}