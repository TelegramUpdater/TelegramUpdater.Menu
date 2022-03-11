using TelegramUpdater.Menu;
using TelegramUpdater.Menu.Directors;
using Xunit;

namespace Tests;

public class UnitTest1
{
    private readonly DirectoryManager _directoryManager;

    public UnitTest1()
    {
        var mainMenu = new MainMenu(
            "Main menu",
            "This is a test menu",
            "TestMenu",
            menus: new IMenu[][]
            {
                new IMenu[]
                {
                    new SubMenu("banana", "🍌", "this is a menu about banana.",
                         new IMenu[][]
                         {
                             new IMenu[]
                             {
                                 new SubMenu("price", "💰", "The price of banana is 20000$",
                                    otherDirectors: new IDirector[][]
                                    {
                                        new IDirector[]
                                        {
                                            new Back("Return"),
                                            new ToMainMenu("Main menu")
                                        }
                                    })
                             }
                         })
                }
            });

        _directoryManager = new DirectoryManager(null, mainMenu);
    }

    [Fact]
    public void Test_1()
    {
        var menu = _directoryManager.GoToDirectory("~/banana");

        var main = _directoryManager.GoToDirectory("~");

        Assert.NotNull(menu);
    }

    [Fact]
    public void Test_2()
    {
        Assert.Equal("TestMenu", _directoryManager.CurrentCallbackData);
    }

    [Fact]
    public void Test_3()
    {
        var menu = _directoryManager.GoToDirectory("~/banana");

        Assert.Equal("TestMenu_banana", _directoryManager.CurrentCallbackData);
    }
}