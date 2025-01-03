using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MA_Presentation_Avalonia.ViewModels;

namespace MA_Presentation_Avalonia;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void TopBar_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        // Start dragging the window
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            this.BeginMoveDrag(e);
        }
    }

    private void ExitButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
    
    // ToggleSwitch crap that ChatGPT helped me write.
    private void ThemeToggle_Checked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Resources["BackgroundColor"] = new SolidColorBrush(Color.Parse("#404040"));
        Resources["ForegroundColor"] = new SolidColorBrush(Color.Parse("#FFE4A3"));
    }

    private void ThemeToggle_Unchecked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Resources["BackgroundColor"] = new SolidColorBrush(Color.Parse("#FFE4A3"));
        Resources["ForegroundColor"] = new SolidColorBrush(Color.Parse("#404040"));
    }
}