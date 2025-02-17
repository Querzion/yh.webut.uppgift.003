using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MA_Presentation_Avalonia.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    // Learned about the region divides from this video.
    // https://www.youtube.com/watch?v=M_QueSwr1tQ
    
    #region Private Members

    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;
        
    #endregion

    #region Public Properties 

    [ObservableProperty]
    private string _headline = "CONTACT LIST";
    
    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];
    
    #endregion
    
    #region Constructor
    
    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="contactService">The Contact Interface Service</param>
    /// <param name="serviceProvider">The Dependency Injection Service</param>
    
    public ContactListViewModel(IContactService contactService, IServiceProvider serviceProvider)
    // public ContactListViewModel()
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;
        
        _contacts = new ObservableCollection<Contact>(_contactService.GetContacts() ?? Enumerable.Empty<Contact>());
    }

    #endregion

    #region Public Commands
    
    [RelayCommand]
    private void GoToContactAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactAddViewModel>();
    }

    [RelayCommand]
    private void GoToContactDetailsView(Contact contact)
    {
        var contactDetailsViewModel = _serviceProvider.GetRequiredService<ContactDetailsViewModel>();
        contactDetailsViewModel.Contact = contact;
        
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = contactDetailsViewModel;
    }

    [RelayCommand]
    private void RemoveContact(Contact contact)
    {
        // Call DeleteContact to remove the contact from both ObservableCollection and file
        bool isDeleted = _contactService.DeleteContact(contact.Id);
    
        if (isDeleted)
        {
            // If the contact was deleted successfully, update the ObservableCollection
            _contacts.Remove(contact);
        }
        else
        {
            Debug.WriteLine($"Failed to delete contact with ID {contact.Id}");
        }
    }

    #endregion
}