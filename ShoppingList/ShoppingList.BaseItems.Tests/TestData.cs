namespace ShoppingList.BaseItems.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bogus;
    using Moq;
    using ShoppingList.BaseItems.Contracts.Models;
    using ShoppingList.BaseItems.Contracts.Providers;
    using ShoppingList.BaseItems.Factories;

    internal static class TestData
    {
        private static readonly Faker Faker = new();

        public static async Task<IBaseItemDatabase> ClearedBaseItemDatabase()
        {
            var provider = BaseItemDatabaseFactory.Create();
            await provider.ClearAsync();
            return provider;
        }

        public static async Task<(IBaseItemDatabase provider, IBaseItem[] items)> ClearedBaseItemDatabaseWithItems(
            int? itemCount = null
        )
        {
            var provider = await TestData.ClearedBaseItemDatabase();

            var items = TestData.CreateBaseItems(itemCount).ToArray();
            Task.WaitAll(items.Select(item => provider.CreateAsync(item)).ToArray());
            return (provider, items);
        }

        public static IBaseItem CreateBaseItem()
        {
            var id = Guid.NewGuid().ToString();
            var name = TestData.Faker.Random.Word();
            var minRequiredQuantityInStock = TestData.Faker.Random.Int(0);

            var mock = new Mock<IBaseItem>();
            mock.Setup(item => item.Id).Returns(id);
            mock.Setup(item => item.Name).Returns(name);
            mock.Setup(item => item.MinRequiredQuantityInStock).Returns(minRequiredQuantityInStock);

            return mock.Object;
        }

        public static IEnumerable<IBaseItem> CreateBaseItems(int? itemCount = null)
        {
            var testSize = itemCount ??
            TestData.Faker.Random.Int(
                5,
                20);
            return Enumerable.Range(
                    0,
                    testSize)
                .Select(_ => TestData.CreateBaseItem());
        }
    }
}
