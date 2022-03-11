namespace TelegramUpdater.Menu.Exceptions
{
    public sealed class DirectoryNotFound : Exception
    {
        public DirectoryNotFound(string dir)
            : base($"Dir '{dir}' not found.")
        {
        }
    }
}
