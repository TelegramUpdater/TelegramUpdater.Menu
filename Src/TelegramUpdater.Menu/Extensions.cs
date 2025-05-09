using System.Diagnostics.CodeAnalysis;
using TelegramUpdater.Menu;

namespace TelegramUpdater;

/// <summary>
/// Extension methods for menus.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Initialize an <see cref="InlineMenuManager"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="InlineMenuManager"/> will add a callback query handler
    /// ( to the updater ) with regex filter that matches <see cref="MainMenu.Prefix"/>.
    /// </remarks>
    /// <param name="updater">The updater.</param>
    /// <param name="mainMenu">Your main menu.</param>
    public static InlineMenuManager AddMenuManager(
        this IUpdater updater, MainMenu mainMenu) => new(updater, mainMenu);

    /// <summary>
    /// Initialize an <see cref="InlineMenuManager"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="InlineMenuManager"/> will add a callback query handler
    /// ( to the updater ) with regex filter that matches <see cref="MainMenu.Prefix"/>.
    /// </remarks>
    /// <param name="updater">The updater.</param>
    /// <param name="key">
    /// You can then get this <see cref="InlineMenuManager"/>, everywhere that 
    /// <see cref="IUpdater"/> exists.
    /// <para>Eg: <c>(InlineMenuManager)updater[<paramref name="key"/>]</c></para>
    /// </param>
    /// <param name="mainMenu">Your main menu.</param>
    /// <returns></returns>
    public static IUpdater AddMenuManager(
        this IUpdater updater, string key, MainMenu mainMenu)
    { 
        updater[key] = new InlineMenuManager(updater, mainMenu);
        return updater;
    }

    /// <summary>
    /// Try to get an <see cref="InlineMenuManager"/> with an specified <paramref name="key"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="InlineMenuManager"/> must be added before using this key.
    /// ( <see cref="AddMenuManager(IUpdater, string, MainMenu)"/> )
    /// </remarks>
    /// <param name="updater"></param>
    /// <param name="key"></param>
    /// <param name="menuManager"></param>
    /// <returns></returns>
    public static bool TryGetInlineMenuManager(
        this IUpdater updater,
        string key,
        [NotNullWhen(true)] out InlineMenuManager? menuManager)
    {
        if (updater.ContainsKey(key))
        {
            menuManager = (InlineMenuManager)updater[key]!;
            return true;
        }

        menuManager = null;
        return false;
    }
}
