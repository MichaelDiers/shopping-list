namespace ShoppingList.BaseItems.Tests.Factories
{
    using ShoppingList.BaseItems.Contracts.Providers;
    using ShoppingList.BaseItems.Factories;
    using Xunit;

    public class BaseItemProviderFactoryTests
    {
        [Fact]
        public void Create()
        {
            var product = BaseItemProviderFactory.Create(BaseItemDatabaseFactory.Create());
            Assert.IsAssignableFrom<IBaseItemProvider>(product);
        }
    }
}
