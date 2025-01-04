using CommunityToolkit.Mvvm.ComponentModel;

namespace CSharp_Assignment_Redo.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";
}