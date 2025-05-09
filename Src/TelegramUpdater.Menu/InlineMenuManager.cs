using System.Text.RegularExpressions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater.Menu.Exceptions;
using TelegramUpdater.UpdateContainer;

namespace TelegramUpdater.Menu;

public class InlineMenuManager
{
    private readonly MainMenu _mainMenu;
    private readonly IUpdater _updater;
    private readonly Dictionary<string, IMenu> _directories;
    private string _currentPath;

    public InlineMenuManager(IUpdater updater, MainMenu mainMenu)
    {
        _updater = updater ?? throw new ArgumentNullException(nameof(updater));
        _mainMenu = mainMenu ?? throw new ArgumentNullException(nameof(mainMenu));

        _directories = MapDirectories();
        _currentPath = "~";

        _updater.AddSingletonUpdateHandler(
            CallbackQueryFound,
            ReadyFilters.DataMatches(@$"^{_mainMenu.Prefix}"));
    }

    public InlineKeyboardMarkup OpeningMarkup
        => new(((IMenu)_mainMenu).ToButtons(ToCallbackData));

    public MainMenu MainMenu => _mainMenu;

    public string CurrentPath => _currentPath;

    public string CurrentCallbackData => ToCallbackData(_currentPath);

    public Dictionary<string, IMenu> Directories => _directories;

    public IMenu GoToDirectory(string dir)
    {
        bool relative = dir.StartsWith("..") || dir.StartsWith("~");

        string searchDir;
        if (relative)
        {
            searchDir = GetRelativePath(_currentPath, dir);
        }
        else
        {
            searchDir = dir;
        }

        if (!_directories.ContainsKey(searchDir))
            throw new DirectoryNotFound(searchDir);

        _currentPath = searchDir;
        return _directories[searchDir];
    }

    public string ToCallbackData(string managerPath)
    {
        if (string.IsNullOrEmpty(managerPath))
        {
            throw new ArgumentException(
                $"'{nameof(managerPath)}' cannot be null or empty.", nameof(managerPath));
        }

        if (!(managerPath.StartsWith("~") || managerPath.StartsWith("..")))
        {
            throw new InvalidOperationException();
        }

        return managerPath.Replace("~", _mainMenu.Prefix)
            .Replace('/', _mainMenu.Separator);
    }

    public string ToManagerPath(string callbackData)
    {
        if (string.IsNullOrEmpty(callbackData))
        {
            throw new ArgumentException(
                $"'{nameof(callbackData)}' cannot be null or empty.", nameof(callbackData));
        }

        if (!callbackData.StartsWith(_mainMenu.Prefix))
        {
            throw new InvalidOperationException();
        }

        return callbackData.Replace(_mainMenu.Prefix, "~")
            .Replace(_mainMenu.Separator, '/');
    }

    public static string GetRelativePath(string fullPath, string relator)
    {
        if (string.IsNullOrEmpty(fullPath))
        {
            throw new ArgumentException(
                $"'{nameof(fullPath)}' cannot be null or empty.", nameof(fullPath));
        }

        if (string.IsNullOrEmpty(relator))
        {
            throw new ArgumentException(
                $"'{nameof(relator)}' cannot be null or empty.", nameof(relator));
        }

        var relatorParts = relator.Split('/', StringSplitOptions.RemoveEmptyEntries);

        // validate the relator
        var dotsAllowed = true;
        var rootAllowed = true;
        var dotsCount = 0;
        foreach (var part in relatorParts)
        {
            if (!Regex.IsMatch(part, @"([a-z][A-Z])*|(\.\.)|~"))
            {
                throw new RelatorInvalidException(relator);
            }

            if (part != "~" && part != "..")
            {
                rootAllowed = false;
                dotsAllowed = false;
            }
            else
            {
                if (!rootAllowed)
                    throw new RelatorInvalidException(relator);
            }

            if (part != "..")
            {
                dotsAllowed = false;
            }
            else
            {
                if (!dotsAllowed)
                    throw new RelatorInvalidException(relator);
                dotsCount++;
            }
        }

        if (relatorParts[0] == "~")
        {
            return relator;
        }

        var fullPathParts = fullPath.Split('/');

        if (dotsCount >= fullPathParts.Length)
            throw new RelatorInvalidException(relator);

        var s1 = string.Join('/', fullPathParts[..^dotsCount]);

        if (relatorParts[dotsCount..].Length != 0)
            s1 += "/" + string.Join('/', relatorParts[dotsCount..]);

        return s1;
    }

    private async Task CallbackQueryFound(IContainer<CallbackQuery> container)
    {
        var data = container.Update.Data;

        if (data is null) return;

        var path = ToManagerPath(data);
        var menu = GoToDirectory(path);

        var buttons = menu.ToButtons(ToCallbackData);

        await container.Edit(text: menu.Description ?? "No description!",
            inlineKeyboardMarkup: new InlineKeyboardMarkup(buttons));
        await container.Answer(menu.Name);
    }

    private static Dictionary<string, IMenu> GetDirectories(
        IMenu menu, string? tail = default)
    {
        string currentTail;
        if (tail is not null)
        {
            currentTail = tail + $"/{menu.Tag}";
        }
        else
        {
            currentTail = "~";
        }

        if (menu is Director director)
            director.ResetPath(GetRelativePath(currentTail, director.RawPath));

        var directories = new Dictionary<string, IMenu>
        {
            { currentTail, menu }
        };
        menu.SetPath(currentTail);

        if (menu.Menus is not null)
        {
            foreach (var subMenu in menu.Menus.SelectMany(x => x))
            {
                if (subMenu is Director director1)
                {
                    director1.ResetPath(GetRelativePath(currentTail, director1.RawPath));
                    continue;
                }

                foreach (var d in InlineMenuManager.GetDirectories(subMenu, currentTail))
                {
                    directories.Add(d.Key, d.Value);
                    d.Value.SetPath(d.Key);
                }
            }
        }

        return directories;
    }


    private Dictionary<string, IMenu> MapDirectories()
    {
        var directories = new Dictionary<string, IMenu>();
        foreach (var d in InlineMenuManager.GetDirectories(_mainMenu))
        {
            directories.Add(d.Key, d.Value);
        }
        return directories;
    }
}
