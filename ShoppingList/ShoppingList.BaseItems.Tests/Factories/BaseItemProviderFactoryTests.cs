namespace ShoppingList.BaseItems.Tests.Factories
{
    using Moq;
    using ShoppingList.BaseItems.Contracts.Providers;
    using ShoppingList.BaseItems.Factories;
    using Xunit;

    public class BaseItemProviderFactoryTests
    {
        [Fact]
        public void Create()
        {
            var database = new Mock<IBaseItemDatabase>();
            var product = BaseItemProviderFactory.Create(database.Object);
            Assert.IsAssignableFrom<IBaseItemProvider>(product);
        }
    }
}
