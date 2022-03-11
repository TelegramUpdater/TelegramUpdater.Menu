using TelegramUpdater.Menu;
using TelegramUpdater.Menu.Exceptions;
using Xunit;

namespace Tests
{
    public class RelativePathTests
    {
        [Fact]
        public void Should_Handle_One_Back_1()
        {
            var related = DirectoryManager.GetRelativePath(
                "~/banana/price", "..");

            Assert.Equal("~/banana", related);
        }

        [Fact]
        public void Should_Handle_One_Back_2()
        {
            var related = DirectoryManager.GetRelativePath(
                "~/banana", "..");

            Assert.Equal("~", related);
        }

        [Fact]
        public void Should_Handle_Two_Back_1()
        {
            var related = DirectoryManager.GetRelativePath(
                "~/banana/price", "../..");

            Assert.Equal("~", related);
        }

        [Fact]
        public void Should_Handle_Two_Back_With_Leading_1()
        {
            var related = DirectoryManager.GetRelativePath(
                "~/banana/price", "../../apple");

            Assert.Equal("~/apple", related);
        }

        [Fact]
        public void Should_Handle_Leadings_1()
        {
            var related = DirectoryManager.GetRelativePath(
                "~/banana/price", "/apple");

            Assert.Equal("~/banana/price/apple", related);
        }

        [Fact]
        public void Should_Handle_Leadings_2()
        {
            var related = DirectoryManager.GetRelativePath(
                "~/banana/price", "apple");

            Assert.Equal("~/banana/price/apple", related);
        }

        [Fact]
        public void Should_Handle_Leadings_3()
        {
            var related = DirectoryManager.GetRelativePath(
                "~/banana/price", "apple/");

            Assert.Equal("~/banana/price/apple", related);
        }

        [Fact]
        public void Should_Handle_Leadings_4()
        {
            var related = DirectoryManager.GetRelativePath(
                "~/banana/price", "apple/price");

            Assert.Equal("~/banana/price/apple/price", related);
        }

        [Fact]
        public void Should_Not_Go_Behind_Root_1()
            => Assert.Throws<RelatorInvalidException>(() =>
            {
                var related = DirectoryManager.GetRelativePath(
                    "~/banana/price", "../../../apple");
            });

        [Fact]
        public void Should_Not_Go_Behind_Root_2()
            => Assert.Throws<RelatorInvalidException>(() =>
            {
                var related = DirectoryManager.GetRelativePath(
                    "~/banana", "../../");
            });

        [Fact]
        public void Should_Handle_From_Root_1()
        {
            var related = DirectoryManager.GetRelativePath(
                "~/banana/price", "~/price");

            Assert.Equal("~/price", related);
        }

        [Fact]
        public void Should_Handle_From_Root_2()
            => Assert.Throws<RelatorInvalidException>(() =>
            {
                var related = DirectoryManager.GetRelativePath(
                    "~/banana", "~/..");
            });
    }
}
