using TelegramUpdater.Menu.Directors;
using TelegramUpdater.Menu.Options;

namespace TelegramUpdater.Menu.CustomMenus
{
    public sealed class PagingMenu : SubMenu
    {
        public PagingMenu(
            string tag,
            string name,
            string description,
            PagingMenuOptions options = default,
            params string[] partDescriptions)

            : base(tag, name, description, false,
                  partDescriptions.Select((x, i) => new SubMenu(
                     $"{i + 1}", $"{name} {i + 1}", x,
                         !options.ShowAllPagesAtIndex && i != 0,
                         GeneratePageDirectors(i,
                             options.PreviousText,
                             options.NextText,
                             partDescriptions.Length,
                             options.AppendBackButton,
                             options.BackButtonText)
                      )).ToArray())
        {
        }

        public PagingMenu(
            string tag,
            string name,
            string description,
            PagingMenuOptions options = default,
            params (string name, string description)[] partDescriptions)

            : base(tag, name, description, false,
                  partDescriptions.Select((x, i) => new SubMenu(
                     $"{i + 1}", x.name, x.description,
                         !options.ShowAllPagesAtIndex && i != 0,
                         GeneratePageDirectors(i,
                             options.PreviousText,
                             options.NextText,
                             partDescriptions.Length,
                             options.AppendBackButton,
                             options.BackButtonText)
                      )).ToArray())
        {
        }

        public PagingMenu(
            string tag,
            string name,
            string description,
            PagingMenuOptions options = default,
            params string[][] partDescriptions)

            : base(tag, name, description, false,
                  partDescriptions.Select((y, j)=> y.Select((x, i) => new SubMenu(
                    $"{(partDescriptions[..j].Sum(x => x.Length)) + i + 1}",
                    $"{name} {(partDescriptions[..j].Sum(x => x.Length)) + i + 1}", x,
                        !options.ShowAllPagesAtIndex && ((partDescriptions[..j].Sum(x => x.Length)) + i) != 0,
                        GeneratePageDirectors(
                            (partDescriptions[..j].Sum(x => x.Length)) + i,
                            options.PreviousText,
                            options.NextText,
                            partDescriptions.Sum(x => x.Length),
                            options.AppendBackButton,
                            options.BackButtonText)))
                  .ToArray()).ToArray())
        {
        }

        public PagingMenu(
            string tag,
            string name,
            string description,
            PagingMenuOptions options = default,
            params (string name, string description)[][] partDescriptions)

            : base(tag, name, description, false,
                  partDescriptions.Select((y, j) => y.Select((x, i) => new SubMenu(
                     $"{(partDescriptions[..j].Sum(x => x.Length)) + i + 1}", x.name, x.description,
                         !options.ShowAllPagesAtIndex && ((partDescriptions[..j].Sum(x => x.Length)) + i) != 0,
                         GeneratePageDirectors(
                             (partDescriptions[..j].Sum(x => x.Length)) + i,
                             options.PreviousText,
                             options.NextText,
                             partDescriptions.Sum(x=> x.Length),
                             options.AppendBackButton,
                             options.BackButtonText)))
                  .ToArray()).ToArray())
        {
        }

        private static Director[] GeneratePageDirectors(
            int index,
            string previousText,
            string nextText,
            int len,
            bool backButton,
            string backButtonText)
        {
            var directors = new List<Director>();
            var currentPage = index + 1;

            if (index == 0)
            {
                directors.Add(new(nextText, $"../{currentPage + 1}", true));
            }
            else if (index + 1 >= len)
            {
                directors.Add(new(previousText, $"../{currentPage - 1}", true));
            }
            else
            {
                directors.Add(new(previousText, $"../{currentPage - 1}", true));
                directors.Add(new(nextText, $"../{currentPage + 1}", true));
            }

            if (backButton)
                directors.Add(new Back(backButtonText));

            return directors.ToArray();
        }
    }
}
