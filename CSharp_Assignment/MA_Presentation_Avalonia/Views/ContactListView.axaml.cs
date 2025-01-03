using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MA_Presentation_Avalonia.ViewModels;

namespace MA_Presentation_Avalonia.Views;

public partial class ContactListView : UserControl
{
    public ContactListView(ContactListViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

}