using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Sockets;
using Business.Entities;
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
                // UpdateContactOption();
                ReadKey();
                break;
            case "4":
                Dialogs.MenuHeading("Delete Contact");
                // DeleteContactOption();
                ReadKey();
                break;
            case "q":
                Dialogs.MenuHeading("Exit Application");
                WriteLine("Program exit... Press any key to exit...");
                ReadKey();
                isRunning = false;
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
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
                WriteLine($"ID: {contact.Id}");
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

    // private void CreateContactOption()
    // {
    //     Dialogs.MenuHeading("New Contact");
    //
    //     var form = new ContactRegistrationForm
    //     {
    //         FirstName = Dialogs.Prompt("Enter First Name: "),
    //         LastName = Dialogs.Prompt("Enter Last Name: "),
    //         Email = Dialogs.Prompt("Enter Email: "),
    //         Address = Dialogs.Prompt("Enter Address: "),
    //         PostalCode = Dialogs.Prompt("Enter PostalCode: "),
    //         City = Dialogs.Prompt("Enter City: "),
    //         PhoneNumber = Dialogs.Prompt("Enter PhoneNumber: ")
    //     };
    //
    //     var result = _contactService.CreateContact(form);
    //
    //     if (result)
    //     {
    //         WriteLine();
    //         WriteLine($"New Contact successfully created.");
    //     }
    //     else
    //     {
    //         WriteLine();
    //         WriteLine($"New Contact failed to be created. Please try again.");
    //     }
    //     
    //     ReadKey();
    // }
    
    public void CreateContactOption()
    {
        Dialogs.MenuHeading("New Contact");

        // Create an instance of the form
        var form = new ContactRegistrationForm();

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

    // private void UpdateContactOption()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // private void DeleteContactOption()
    // {
    //     throw new NotImplementedException();
    // }
}