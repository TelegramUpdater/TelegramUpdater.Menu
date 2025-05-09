// See https://aka.ms/new-console-template for more information
using MenuBotExample;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramUpdater;
using TelegramUpdater.Menu;
using TelegramUpdater.Menu.CustomMenus;
using TelegramUpdater.Menu.Options;
using TelegramUpdater.UpdateContainer;


var updater = new Updater(
    "BOT_TOKEN",
    preUpdateProcessorType: typeof(MyPreProcessor));


var pagingMenuOptions = new PagingMenuOptions(
    previousText: "⬅️",
    nextText: "➡️",
    showAllPagesAtIndex: true,
    appendBackButton: true,
    backButtonText: "↩️");

var myMenu = new MainMenu(
    "Main menu",
    "This is a test menu",
    "TestMenu",
    menu: new PagingMenu(
        "Docs", "Documentations", "Read my docs here.",
        pagingMenuOptions,
        [("🍕", "Documentations on part 1")],
        [("🍔", "Documentations on part 2"), ("🌭", "Documentations on part 3")],
        [("🍟", "Documentations on part 4"), ("🍿", "Documentations on part 5"), ("🍳", "Documentations on part 6")]));


updater.AddMenuManager("myPagingMenu", myMenu);

updater.AddSingletonUpdateHandler(
    UpdateType.Message,
    ShowMenuHandler,
    ReadyFilters.OnCommand("menu"));


static async Task ShowMenuHandler(IContainer<Message> container)
{
    if (container.Updater.TryGetInlineMenuManager("myPagingMenu", out var manager))
    {
        await container.Response(
            manager.MainMenu.Description,
            replyMarkup: manager.OpeningMarkup);
    }
}

await updater.Start();