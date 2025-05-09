namespace TelegramUpdater.Menu;

public class Director : IMenu
{
    public Director(string name, string path, bool relative, bool hidden = false)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(
                $"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException(
                $"'{nameof(path)}' cannot be null or empty.", nameof(path));
        }

        Name = name;
        RawPath = path;
        Relative = relative;
        Hidden = hidden;
    }

    public void ResetPath(string path)
    {
        ((IMenu)this).SetPath(path);
    }

    public string RawPath { get; }

    public string Name { get; }

    string IMenu.Path { get; set; } = default!;

    public bool Relative { get; }

    public string Tag => null!;

    public string Description => null!;

    public IEnumerable<IEnumerable<IMenu>>? Menus => null;

    public bool Hidden { get; }
}
