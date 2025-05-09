// See https://aka.ms/new-console-template for more information
using Telegram.Bot.Types.Enums;
using TelegramUpdater;
using TelegramUpdater.Menu;
using TelegramUpdater.Menu.Directors;
using TelegramUpdater.UpdateContainer;

var updater = new Updater("BOT_TOKEN");


var mainMenu = new MainMenu(
    "Simple Menu",
    "Ready to Go? Then what are you waiting for?",
    "SimpleMenu",
    options: default,
    menu: new SubMenu(
        "Gone", "Go",
        "Aw so you're Gone, you better come back!",
        menu: new Back("Come")));

updater.AddMenuManager("myMenu", mainMenu);

updater.AddSingletonUpdateHandler(
    UpdateType.Message,
    async cntr =>
    {
        if (cntr.Updater.TryGetInlineMenuManager("myMenu", out var manager))
        {
            await cntr.Response(
                manager.MainMenu.Description,
                replyMarkup: manager.OpeningMarkup);
        }
    },
    ReadyFilters.OnCommand("menu"));

await updater.Start();