using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MA_Presentation_Avalonia.ViewModels;

public partial class ContactAddViewModel(IContactService contactService, IServiceProvider serviceProvider)
    : ObservableObject
{
    private readonly IContactService _contactService = contactService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    [ObservableProperty]
    private string headline = "ADD CONTACT";
    
    [ObservableProperty]
    private string warningMessage = "";
    
    [ObservableProperty] 
    private ContactRegistrationForm _contact = new();
    
    // [RelayCommand]
    // private void AddContact()
    // {
    //     if (!string.IsNullOrEmpty(Contact.FirstName))
    //     {
    //         var result = _contactService.CreateContact(Contact);
    //         if (result)
    //         {
    //             BackOrCancel();
    //         }
    //     }
    //     else
    //     {
    //         WarningMessage = "The fields cannot be empty if you want to add a contact.";
    //     }
    // }
    
    // [RelayCommand]
    // private void AddContact()
    // {
    //     var result = _contactService.CreateContact(_contact);
    //     if (result)
    //     {
    //         BackOrCancel();
    //     }
    //     else
    //     {
    //         WarningMessage = "The fields cannot be empty if you want to add a contact.";
    //     }
    // }
    
    [RelayCommand]
    private void AddContact()
    {
        // Create a validation context for the _contact object
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(_contact, null, null);

        // Validate the model
        bool isValid = Validator.TryValidateObject(_contact, validationContext, validationResults, true);

        if (isValid)
        {
            // Proceed with creating the contact if validation passes
            var result = _contactService.CreateContact(_contact);
            if (result)
            {
                BackOrCancel();
            }
            else
            {
                WarningMessage = "There was an issue saving the contact.";
            }
        }
        else
        {
            // If validation fails, show the error messages
            WarningMessage = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
        }
    }


    [RelayCommand]
    private void BackOrCancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}