namespace ShoppingList.BaseItems.Tests.Providers
{
    using System;
    using System.Threading.Tasks;
    using Bogus;
    using Moq;
    using ShoppingList.BaseItems.Contracts.Models;
    using ShoppingList.BaseItems.Contracts.Providers;
    using ShoppingList.BaseItems.Factories;
    using ShoppingList.BaseItems.Requests;
    using ShoppingList.Shared.Exceptions;
    using Xunit;

    public class BaseItemProviderTests
    {
        private readonly Faker faker = new();

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task CreateAsync(bool baseItemAlreadyExists)
        {
            var createRequest = new CreateRequest
            {
                MinRequiredQuantityInStock = this.faker.Random.Int(
                    1,
                    20),
                Name = this.faker.Random.Word()
            };
            var database = new Mock<IBaseItemDatabase>();
            if (baseItemAlreadyExists)
            {
                database.Setup(x => x.CreateAsync(It.IsAny<IBaseItem>())).Throws<AlreadyExistsException>();
            }
            else
            {
                database.Setup(x => x.CreateAsync(It.IsAny<IBaseItem>())).Returns(Task.CompletedTask);
            }

            var provider = BaseItemProviderFactory.Create(database.Object);

            if (baseItemAlreadyExists)
            {
                await Assert.ThrowsAsync<AlreadyExistsException>(() => provider.CreateAsync(createRequest));
            }
            else
            {
                var baseItem = await provider.CreateAsync(createRequest);

                Assert.Equal(
                    createRequest.MinRequiredQuantityInStock,
                    baseItem.MinRequiredQuantityInStock);
                Assert.Equal(
                    createRequest.Name,
                    baseItem.Name);
                Assert.True(
                    Guid.TryParse(
                        baseItem.Id,
                        out var guid) &&
                    guid != Guid.Empty);
            }
        }
    }
}
