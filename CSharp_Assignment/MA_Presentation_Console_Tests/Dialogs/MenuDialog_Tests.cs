using Business.Interfaces;
using MA_Presentation_Console.Dialogs;
using Moq;

namespace MA_Presentation_Console_Tests.Dialogs;

public class MenuDialog_Tests
{
    private readonly Mock<IContactService> _mockContactService;
    private readonly MenuDialog _menuDialog;

    public MenuDialog_Tests()
    {
        _mockContactService = new Mock<IContactService>();
        _menuDialog = new MenuDialog(_mockContactService.Object);
    }

    [Fact]
    public void ShowMainMenu_ShouldReturnMainMenu()
    {
        
    }
    
    
}