using TelegramUpdater.Menu.Options;

namespace TelegramUpdater.Menu;

/// <summary>
/// The main menu.
/// </summary>
/// <remarks>
/// Create a new instance of <see cref="MainMenu"/>.
/// </remarks>
/// <param name="name"></param>
/// <param name="description"></param>
/// <param name="prefix"></param>
/// <param name="options"></param>
/// <param name="menus"></param>
public sealed class MainMenu(
    string name,
    string description,
    string prefix,
    MainMenuOptions options = default,
    params IMenu[][]? menus) : AbstractMenu("~", name, description, options.Hidden, menus)
{
    /// <summary>
    /// Create a new instance of <see cref="MainMenu"/>.
    /// </summary>
    public MainMenu(
        string name,
        string description,
        string prefix,
        MainMenuOptions options = default,
        params IMenu[]? menus)
        : this(name, description, prefix, options, menus is not null?
        [
            menus
        ]: [])
    { }

    /// <summary>
    /// Create a new instance of <see cref="MainMenu"/>.
    /// </summary>
    public MainMenu(
        string name,
        string description,
        string prefix,
        MainMenuOptions options = default,
        IMenu? menu = null)
        : this(name, description, prefix, options, menu is not null ?
        [
            [menu]
        ] : [])
    { }

    /// <summary>
    /// Prefix
    /// </summary>
    public string Prefix { get; } = prefix;

    /// <summary>
    /// Separator.
    /// </summary>
    public char Separator { get; } = options.Separator;
}
