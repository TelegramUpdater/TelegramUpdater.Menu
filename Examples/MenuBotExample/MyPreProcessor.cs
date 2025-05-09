using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using TelegramUpdater;
using TelegramUpdater.RainbowUtilities;

namespace MenuBotExample
{
    internal sealed class MyPreProcessor : AbstractPreUpdateProcessor
    {
        public override Task<bool> PreProcessor(ShiningInfo<long, Update> shiningInfo)
        {
            switch (shiningInfo)
            {
                case { Value.CallbackQuery: { Data: { } data } callback }:
                    {
                        Updater.Logger.LogInformation("New callback with {data}", data);
                        break;
                    }
            }

            return Task.FromResult(true);
        }
    }
}
