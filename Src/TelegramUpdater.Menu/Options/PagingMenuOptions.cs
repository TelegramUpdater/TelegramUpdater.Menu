namespace TelegramUpdater.Menu.Options;

public readonly struct PagingMenuOptions
{
    public PagingMenuOptions(
        string previousText = "Previous",
        string nextText = "Next",
        bool showAllPagesAtIndex = false,
        bool appendBackButton = true,
        string backButtonText = "Back")
    {
        if (string.IsNullOrEmpty(previousText))
        {
            throw new ArgumentException(
                $"'{nameof(previousText)}' cannot be null or empty.", nameof(previousText));
        }

        if (string.IsNullOrEmpty(nextText))
        {
            throw new ArgumentException(
                $"'{nameof(nextText)}' cannot be null or empty.", nameof(nextText));
        }

        if (string.IsNullOrEmpty(backButtonText))
        {
            throw new ArgumentException(
                $"'{nameof(backButtonText)}' cannot be null or empty.", nameof(backButtonText));
        }

        PreviousText = previousText;
        NextText = nextText;
        ShowAllPagesAtIndex = showAllPagesAtIndex;
        AppendBackButton = appendBackButton;
        BackButtonText = backButtonText;
    }

    public string PreviousText { get; } = "Previous";

    public string NextText { get; } = "Next";

    public string BackButtonText { get; } = "Back";

    public bool ShowAllPagesAtIndex { get; } = false;

    public bool AppendBackButton { get; } = true;
}
