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

    private void ViewContactsOption()
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

    private void CreateContactOption()
    {
        Dialogs.MenuHeading("New Contact");

        var form = new ContactRegistrationForm
        {
            FirstName = Dialogs.Prompt("Enter First Name: "),
            LastName = Dialogs.Prompt("Enter Last Name: "),
            Email = Dialogs.Prompt("Enter Email: "),
            Address = Dialogs.Prompt("Enter Address: "),
            PostalCode = Dialogs.Prompt("Enter PostalCode: "),
            City = Dialogs.Prompt("Enter City: "),
            PhoneNumber = Dialogs.Prompt("Enter PhoneNumber: ")
        };

        var result = _contactService.CreateContact(form);

        if (result)
        {
            WriteLine();
            WriteLine($"New Contact successfully created.");
        }
        else
        {
            WriteLine();
            WriteLine($"New Contact failed to be created. Please try again.");
        }
        
        ReadKey();
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