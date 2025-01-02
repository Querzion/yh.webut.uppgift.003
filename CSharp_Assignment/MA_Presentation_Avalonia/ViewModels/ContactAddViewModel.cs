using System;
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
    private ContactRegistrationForm _contact = new();

    [RelayCommand]
    private void AddContact()
    {
        var result = _contactService.CreateContact(Contact);
        if (result)
        {
            BackOrCancel();
        }
    }

    [RelayCommand]
    private void BackOrCancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}