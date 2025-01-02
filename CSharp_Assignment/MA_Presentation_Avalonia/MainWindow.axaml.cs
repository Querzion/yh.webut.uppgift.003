using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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
}