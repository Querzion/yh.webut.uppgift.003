using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using MA_Presentation_Avalonia.ViewModels;
using MA_Presentation_Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MA_Presentation_Avalonia;

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register file and contact services
                services.AddSingleton<IContactFileService>(new ContactFileService("../../../../Data", "Contacts.json"));
                services.AddSingleton<IContactRepository, ContactRepository>();
                services.AddSingleton<IContactService, ContactService>();
                
                // Register main view model and window
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
                
                // Register view models and views
                services.AddTransient<ContactListViewModel>();
                services.AddTransient<ContactListView>();

                services.AddTransient<ContactAddViewModel>();
                services.AddTransient<ContactAddView>();
                
                services.AddTransient<ContactDetailsViewModel>();
                services.AddTransient<ContactDetailsView>();
                
                services.AddTransient<ContactEditViewModel>();
                services.AddTransient<ContactEditView>();
                
            })
            .Build();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // ChatGPT Wants me to add this.
        if (_host == null)
        {
            throw new InvalidOperationException("Host is not initialized.");
        }
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Sometimes ChatGPT gets it kind of functional. 
            try
            {
                if (_host == null)
                {
                    throw new InvalidOperationException("Host is not initialized.");
                }
                
                var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
                var contactListViewModel = _host.Services.GetRequiredService<ContactListViewModel>();
                var mainWindow = _host.Services.GetRequiredService<MainWindow>();

                mainViewModel.CurrentViewModel = contactListViewModel;
                mainWindow.DataContext = mainViewModel;
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Debug.WriteLine($"Error during startup: {ex.Message}");
                throw;
            }
            
            // var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            // mainWindow.Show();
        }

        base.OnFrameworkInitializationCompleted();
    }
}