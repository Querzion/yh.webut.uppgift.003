using System.Diagnostics;
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
                ReadKey();
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
        Dialogs.MenuHeading("Contacts");

        try
        {
            var contacts = _contactService.GetContacts();

            if (contacts.Any())
            {
                foreach (var contact in contacts)
                {
                    WriteLine(
                        $"ID: {contact.Id}, Name: {contact.Name}, Full Address: {contact.FullAddress}, Email: <{contact.Email}>, Phone Number: {contact.PhoneNumber} ");
                }
            }
            else
            {
                WriteLine("There are no contacts.");
            }
        }
        // Multiple catches are from ChatGPT
        catch (DirectoryNotFoundException ex)
        {
            Debug.WriteLine($"Error: {ex.Message}. The contacts file could not be found.");
            throw;
        }
        catch (FileNotFoundException ex)
        {
            Debug.WriteLine($"Error: {ex.Message}. The contacts file is missing.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An unexpected error occured: {ex.Message}.");
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
            WriteLine($"New Contact successfully created.");
        }
        else
        {
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