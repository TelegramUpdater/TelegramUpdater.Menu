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
        "Docs", "Documentions", "Read my docs here.",
        pagingMenuOptions,
        new[] { ("🍕", "Documentions on part 1") },
        new[] { ("🍔", "Documentions on part 2"), ("🌭", "Documentions on part 3") },
        new[] { ("🍟", "Documentions on part 4"), ("🍿", "Documentions on part 5"), ("🍳", "Documentions on part 6") }));


updater.AddMenuManager("myPagingMenu", myMenu);

updater.AddSingletonUpdateHandler(
    UpdateType.Message,
    ShowMenuHandler,
    FilterCutify.OnCommand("menu"));


static async Task ShowMenuHandler(IContainer<Message> container)
{
    if (container.Updater.TryGetInlineMenuManager("myPagingMenu", out var manager))
    {
        await container.ResponseAsync(
            manager.MainMenu.Description,
            replyMarkup: manager.OpeningMarkup);
    }
}

await updater.StartAsync();