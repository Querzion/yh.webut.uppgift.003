using static System.Console;

namespace MA_Presentation_Console.Dialogs;

public class MenuDialog
{
    public void ShowMainMenu()
    {
        while (true)
        {
            MainDialogOptions();
        }
    }

    private void MainDialogOptions()
    {
        Dialogs.MenuHeading("Main Menu");
        WriteLine("1. Create Contact");
        WriteLine("2. View Contacts");
        WriteLine("___________________________________________");
        var option = ReadLine()!;

        switch (option)
        {
            case "1":
                break;
            case "2":
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
    
    
}