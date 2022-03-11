namespace TelegramUpdater.Menu.Exceptions
{
    public sealed class RelatorInvalidException : Exception
    {
        public RelatorInvalidException(string relator)
            : base($"The relator is invalid: {relator}")
        {
        }
    }
}
