using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using MA_Presentation_Avalonia.ViewModels;
using MA_Presentation_Avalonia.Views;

namespace MA_Presentation_Avalonia;

public partial class App : Application
{
    private readonly IHost? _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register file and contact services
                services.AddSingleton<IContactFileService>(new ContactFileService(AppDomain.CurrentDomain.BaseDirectory, "contacts.json"));
                services.AddSingleton<IContactRepository, ContactRepository>();
                services.AddSingleton<IContactService, ContactService>();
                
                // Register view models
                services.AddTransient<ContactListViewModel>();
                services.AddTransient<ContactAddViewModel>();
                services.AddTransient<ContactDetailsViewModel>();
                services.AddTransient<ContactEditViewModel>();

                // Register views
                services.AddTransient<ContactListView>();
                services.AddTransient<ContactAddView>();
                services.AddTransient<ContactDetailsView>();
                services.AddTransient<ContactEditView>();

                // Register mainviewmodel and window
                services.AddTransient<MainViewModel>();
                services.AddTransient<MainWindow>();

            })
            .Build();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Added a ! here in order to dereference a possible null. Application starts, but I add the memo anyways.
            var mainViewModel = _host!.Services.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ContactListViewModel>();
            
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }

        base.OnFrameworkInitializationCompleted();
    }
}