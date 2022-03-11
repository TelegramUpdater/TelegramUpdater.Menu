namespace TelegramUpdater.Menu.Options;

public readonly struct MainMenuOptions
{
    public MainMenuOptions(char separator = '_', bool hidden = false)
    {
        Separator = separator;
        Hidden = hidden;
    }

    public char Separator { get; } = '_';

    public bool Hidden { get; } = false;
}
