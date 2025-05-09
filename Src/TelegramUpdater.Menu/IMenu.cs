using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramUpdater.Menu;

/// <summary>
/// Base interface for menus.
/// </summary>
public interface IMenu
{
    public string Name { get; }

    internal string Path { get; set; }

    public bool Relative { get; }

    internal void SetPath(string path)
    {
        Path = path;
    }

    public string Tag { get; }
    
    /// <summary>
    /// Description of the menu.
    /// </summary>
    public string Description { get; }

    public bool Hidden { get; }
    
    /// <summary>
    /// A list of directors.
    /// </summary>
    public IEnumerable<IEnumerable<IMenu>>? Menus { get; }


    public IEnumerable<IEnumerable<InlineKeyboardButton>> ToButtons(
        Func<string, string> getCallbackFormPath)
    {
        return Menus?.Select(x => x.Where(x=> !x.Hidden)
            .Select(y => InlineKeyboardButton.WithCallbackData(
                y.Name, getCallbackFormPath(y.Path))))
            ?? [];
    }
}