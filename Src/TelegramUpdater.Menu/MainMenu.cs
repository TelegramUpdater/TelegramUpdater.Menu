using TelegramUpdater.Menu.Options;

namespace TelegramUpdater.Menu;

public sealed class MainMenu: AbstractMenu
{
    public MainMenu(
        string name,
        string description,
        string prefix,
        MainMenuOptions options = default,
        params IMenu[][]? menus)
        : base("~", name, description, options.Hidden, menus)
    {
        Prefix = prefix;
        Separator = options.Separator;
    }

    public MainMenu(
        string name,
        string description,
        string prefix,
        MainMenuOptions options = default,
        params IMenu[]? menus)
        : this(name, description, prefix, options, menus is not null? new IMenu[][]
        {
            menus
        }: Array.Empty<IMenu[]>())
    { }

    public MainMenu(
        string name,
        string description,
        string prefix,
        MainMenuOptions options = default,
        IMenu? menu = null)
        : this(name, description, prefix, options, menu is not null ? new IMenu[][]
        {
            new IMenu[] { menu }
        } : Array.Empty<IMenu[]>())
    { }

    public string Prefix { get; }

    public char Separator { get; }
}
