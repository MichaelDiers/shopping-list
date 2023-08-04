namespace ShoppingList.BaseItems.Tests.Factories
{
    using ShoppingList.BaseItems.Contracts.Providers;
    using ShoppingList.BaseItems.Factories;
    using Xunit;

    public class BaseItemDatabaseFactoryTests
    {
        [Fact]
        public void Create()
        {
            var product = BaseItemDatabaseFactory.Create();
            Assert.IsAssignableFrom<IBaseItemDatabase>(product);
        }
    }
}
