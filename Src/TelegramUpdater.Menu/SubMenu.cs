namespace TelegramUpdater.Menu;

public class SubMenu : AbstractMenu
{
    public SubMenu(string tag,
                   string name,
                   string description,
                   bool hidden = false,
                   params IMenu[][]? menus)
        : base(tag, name, description, hidden, menus)
    {
    }

    public SubMenu(string tag,
                   string name,
                   string description,
                   bool hidden = false,
                   params IMenu[]? menus)
        : this(tag, name, description, hidden, menus is not null ? new IMenu[][]
        {
            menus
        } : Array.Empty<IMenu[]>())
    {
    }

    public SubMenu(string tag,
                   string name,
                   string description,
                   bool hidden = false,
                   IMenu? menu = null)
        : this(tag, name, description, hidden, menu is not null ? new IMenu[][]
        {
            new IMenu[] { menu }
        } : Array.Empty<IMenu[]>())
    {
    }
}