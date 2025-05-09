namespace TelegramUpdater.Menu;

/// <summary>
/// Abstract base for <see cref="IMenu"/>.
/// </summary>
public abstract class AbstractMenu: IMenu
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="hidden"></param>
    /// <param name="menus"></param>
    /// <exception cref="ArgumentException"></exception>
    protected AbstractMenu(
        string tag,
        string name,
        string description,
        bool hidden = false,
        IEnumerable<IEnumerable<IMenu>>? menus = default)
    {
        if (string.IsNullOrEmpty(tag))
        {
            throw new ArgumentException(
                $"'{nameof(tag)}' cannot be null or empty.", nameof(tag));
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(
                $"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrEmpty(description))
        {
            throw new ArgumentException(
                $"'{nameof(description)}' cannot be null or empty.", nameof(description));
        }

        Tag = tag;
        Name = name;
        Description = description;
        Menus = menus;
        Hidden = hidden;
    }

    /// <inheritdoc/>
    public string Name { get; }
    
    /// <inheritdoc/>
    public string Description { get; }
    
    /// <inheritdoc/>
    public IEnumerable<IEnumerable<IMenu>>? Menus { get; }

    /// <inheritdoc/>
    public string Tag { get; }

    /// <inheritdoc/>
    string IMenu.Path { get; set; } = default!;

    /// <inheritdoc/>
    public bool Relative => false;

    /// <inheritdoc/>
    public bool Hidden { get; }
}