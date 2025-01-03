using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MA_Presentation_Avalonia.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    
    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
    
    
    // ReactiveColor Commands for switching between dark and light mode.
    private SolidColorBrush _backgroundBrush = new SolidColorBrush(Color.Parse("#FFE4A3"));
    private SolidColorBrush _foregroundBrush = new SolidColorBrush(Color.Parse("#404040"));

    public SolidColorBrush BackgroundBrush
    {
        get => _backgroundBrush;
        set => SetProperty(ref _backgroundBrush, value);
    }

    public SolidColorBrush ForegroundBrush
    {
        get => _foregroundBrush;
        set => SetProperty(ref _foregroundBrush, value);
    }

    [RelayCommand]
    private void ToggleColors(bool isChecked)
    {
        BackgroundBrush = isChecked
            ? new SolidColorBrush(Color.Parse("#404040"))
            : new SolidColorBrush(Color.Parse("#FFE4A3"));

        ForegroundBrush = isChecked
            ? new SolidColorBrush(Color.Parse("#FFE4A3"))
            : new SolidColorBrush(Color.Parse("#404040"));
    }
}