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

public partial class ContactEditViewModel(IContactService contactService, IServiceProvider serviceProvider)
    : ObservableObject
{
    private readonly IContactService _contactService = contactService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private string _headline = "EDIT CONTACT";
    
    [ObservableProperty]
    private string _warningMessage = "";
    
    [ObservableProperty] 
    private Contact _contact = new();

    [RelayCommand]
    private void UpdateContact()
    {
        // Memo: Changed _contact to Contact in three places.
        
        // Create a validation context for the _contact object
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(Contact, null, null);

        // Validate the model
        bool isValid = Validator.TryValidateObject(Contact, validationContext, validationResults, true);

        if (isValid)
        {
            // Proceed with creating the contact if validation passes
            var result = _contactService.UpdateContact(Contact);
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