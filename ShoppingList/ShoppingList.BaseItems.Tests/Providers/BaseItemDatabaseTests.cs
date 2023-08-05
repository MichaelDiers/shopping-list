namespace ShoppingList.BaseItems.Tests.Providers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using ShoppingList.BaseItems.Contracts.Models;
    using ShoppingList.Shared.Exceptions;
    using Xunit;

    public class BaseItemDatabaseTests
    {
        [Fact]
        public async Task CreateAsync_ExistingBaseItem_Fail()
        {
            var provider = await TestData.ClearedBaseItemDatabase();
            var baseItem = TestData.CreateBaseItem();

            await provider.CreateAsync(baseItem);
            await Assert.ThrowsAsync<AlreadyExistsException>(() => provider.CreateAsync(baseItem));
        }

        [Fact]
        public async Task CreateAsync_NewBaseItem_Ok()
        {
            var provider = await TestData.ClearedBaseItemDatabase();
            var baseItem = TestData.CreateBaseItem();

            await provider.CreateAsync(baseItem);
            var created = await provider.ReadAsync(baseItem.Id);

            Assert.Equal(
                baseItem.Id,
                created.Id);
            Assert.Equal(
                baseItem.MinRequiredQuantityInStock,
                created.MinRequiredQuantityInStock);
            Assert.Equal(
                baseItem.Name,
                created.Name);
        }

        [Fact]
        public async Task DeleteAsync_UsingEmptyDatabase_Fail()
        {
            var provider = await TestData.ClearedBaseItemDatabase();
            await Assert.ThrowsAsync<NotFoundException>(() => provider.DeleteAsync(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task DeleteAsync_UsingNonEmptyDatabase_Fail()
        {
            var (provider, _) = await TestData.ClearedBaseItemDatabaseWithItems();

            await Assert.ThrowsAsync<NotFoundException>(() => provider.DeleteAsync(TestData.CreateBaseItem().Id));
        }

        [Fact]
        public async Task DeleteAsync_UsingNonEmptyDatabase_Ok()
        {
            var (provider, baseItems) = await TestData.ClearedBaseItemDatabaseWithItems();
            Task.WaitAll(baseItems.Select(item => provider.DeleteAsync(item.Id)).ToArray());
        }

        [Fact]
        public async Task DeleteAsync_UsingSingleItemDatabase_Fail()
        {
            var (provider, _) = await TestData.ClearedBaseItemDatabaseWithItems(1);
            await Assert.ThrowsAsync<NotFoundException>(() => provider.DeleteAsync(TestData.CreateBaseItem().Id));
        }

        [Fact]
        public async Task DeleteAsync_UsingSingleItemDatabase_Ok()
        {
            var (provider, baseItems) = await TestData.ClearedBaseItemDatabaseWithItems(1);
            await provider.DeleteAsync(baseItems[0].Id);
            await Assert.ThrowsAsync<NotFoundException>(() => provider.DeleteAsync(baseItems[0].Id));
        }

        [Fact]
        public async Task ReadAsync_UsingEmptyDatabase_ForUnknownId_Fail()
        {
            var provider = await TestData.ClearedBaseItemDatabase();

            await Assert.ThrowsAsync<NotFoundException>(() => provider.ReadAsync(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task ReadAsync_UsingEmptyDatabase_Ok()
        {
            var provider = await TestData.ClearedBaseItemDatabase();
            var items = await provider.ReadAsync();
            Assert.Empty(items);
        }

        [Fact]
        public async Task ReadAsync_UsingNonEmptyDatabase_ForKnownId_Ok()
        {
            var (provider, expectedItems) = await TestData.ClearedBaseItemDatabaseWithItems();

            Task.WaitAll(expectedItems.Select(item => provider.ReadAsync(item.Id) as Task).ToArray());
        }

        [Fact]
        public async Task ReadAsync_UsingNonEmptyDatabase_ForUnknownId_Fail()
        {
            var (provider, _) = await TestData.ClearedBaseItemDatabaseWithItems();

            await Assert.ThrowsAsync<NotFoundException>(() => provider.ReadAsync(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task ReadAsync_UsingNonEmptyDatabase_Ok()
        {
            var (provider, expectedItems) = await TestData.ClearedBaseItemDatabaseWithItems();

            var createdItems = (await provider.ReadAsync()).ToArray();
            Assert.Equal(
                expectedItems.Length,
                createdItems.Length);
            Assert.True(
                expectedItems.All(
                    expected => createdItems.Any(
                        created => created.Id == expected.Id &&
                                   created.MinRequiredQuantityInStock == expected.MinRequiredQuantityInStock &&
                                   created.Name == expected.Name)));
        }

        [Fact]
        public async Task ReadAsync_UsingSingleItemDatabase_ForKnownId()
        {
            var (provider, baseItems) = await TestData.ClearedBaseItemDatabaseWithItems(1);

            var found = await provider.ReadAsync(baseItems[0].Id);

            this.AssertBaseItems(
                baseItems[0],
                found);
        }

        [Fact]
        public async Task UpdateAsync_UsingEmptyDatabase_Fail()
        {
            var provider = await TestData.ClearedBaseItemDatabase();
            await Assert.ThrowsAsync<NotFoundException>(() => provider.UpdateAsync(TestData.CreateBaseItem()));
        }

        [Fact]
        public async Task UpdateAsync_UsingNonEmptyDatabase_Fail()
        {
            var (provider, _) = await TestData.ClearedBaseItemDatabaseWithItems();

            await Assert.ThrowsAsync<NotFoundException>(() => provider.UpdateAsync(TestData.CreateBaseItem()));
        }

        [Fact]
        public async Task UpdateAsync_UsingNonEmptyDatabase_Ok()
        {
            var (provider, baseItems) = await TestData.ClearedBaseItemDatabaseWithItems();

            var mockItem = new Mock<IBaseItem>();
            mockItem.Setup(item => item.Id).Returns(baseItems[0].Id);
            mockItem.Setup(item => item.Name).Returns($"New: {baseItems[0].Name}");
            mockItem.Setup(item => item.MinRequiredQuantityInStock)
                .Returns(baseItems[0].MinRequiredQuantityInStock + 1);

            await provider.UpdateAsync(mockItem.Object);
            var updated = await provider.ReadAsync(mockItem.Object.Id);

            this.AssertBaseItems(
                mockItem.Object,
                updated);
        }

        [Fact]
        public async Task UpdateAsync_UsingSingleItemDatabase_Fail()
        {
            var (provider, _) = await TestData.ClearedBaseItemDatabaseWithItems(1);

            await Assert.ThrowsAsync<NotFoundException>(() => provider.UpdateAsync(TestData.CreateBaseItem()));
        }

        [Fact]
        public async Task UpdateAsync_UsingSingleItemDatabase_Ok()
        {
            var (provider, baseItems) = await TestData.ClearedBaseItemDatabaseWithItems(1);

            var mockItem = new Mock<IBaseItem>();
            mockItem.Setup(item => item.Id).Returns(baseItems[0].Id);
            mockItem.Setup(item => item.Name).Returns($"New: {baseItems[0].Name}");
            mockItem.Setup(item => item.MinRequiredQuantityInStock)
                .Returns(baseItems[0].MinRequiredQuantityInStock + 1);

            await provider.UpdateAsync(mockItem.Object);
            var updated = await provider.ReadAsync(mockItem.Object.Id);

            this.AssertBaseItems(
                mockItem.Object,
                updated);
        }

        private void AssertBaseItems(IBaseItem expected, IBaseItem actual)
        {
            Assert.Equal(
                expected.Id,
                actual.Id);
            Assert.Equal(
                expected.Name,
                actual.Name);
            Assert.Equal(
                expected.MinRequiredQuantityInStock,
                actual.MinRequiredQuantityInStock);
        }
    }
}
