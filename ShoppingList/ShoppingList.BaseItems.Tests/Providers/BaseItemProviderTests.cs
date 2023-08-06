namespace ShoppingList.BaseItems.Tests.Providers
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
    using ShoppingList.BaseItems.Requests;
    using ShoppingList.Shared.Exceptions;
    using Xunit;

    public class BaseItemProviderTests
    {
        private readonly Faker faker = new();

        [Fact]
        public async Task ClearAsync()
        {
            var database = new Mock<IBaseItemDatabase>();
            database.Setup(db => db.ClearAsync()).Returns(Task.CompletedTask);
            var provider = BaseItemProviderFactory.Create(database.Object);

            await provider.ClearAsync();
        }

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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task DeleteAsync(bool exists)
        {
            var database = new Mock<IBaseItemDatabase>();
            var id = Guid.NewGuid().ToString();
            if (exists)
            {
                database.Setup(x => x.DeleteAsync(id)).Returns(Task.CompletedTask);
            }
            else
            {
                database.Setup(x => x.DeleteAsync(It.IsNotIn(id))).ThrowsAsync(new NotFoundException());
            }

            var provider = BaseItemProviderFactory.Create(database.Object);

            if (exists)
            {
                await provider.DeleteAsync(id);
            }
            else
            {
                await Assert.ThrowsAsync<NotFoundException>(() => provider.DeleteAsync(Guid.NewGuid().ToString()));
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public async Task ReadAsync(int size)
        {
            var database = new Mock<IBaseItemDatabase>();
            var expected = Enumerable.Range(
                    0,
                    size)
                .Select(_ => new Mock<IBaseItem>().Object);
            database.Setup(x => x.ReadAsync()).ReturnsAsync(expected);
            var provider = BaseItemProviderFactory.Create(database.Object);

            var actual = await provider.ReadAsync();

            Assert.Equal(
                size,
                actual.Count());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ReadAsync_UsingId(bool exists)
        {
            var database = new Mock<IBaseItemDatabase>();
            var id = Guid.NewGuid().ToString();
            if (exists)
            {
                var baseItemMock = new Mock<IBaseItem>();
                baseItemMock.Setup(x => x.Id).Returns(id);
                database.Setup(x => x.ReadAsync(id)).ReturnsAsync(baseItemMock.Object);
            }
            else
            {
                database.Setup(x => x.ReadAsync(It.IsAny<string>())).ThrowsAsync(new NotFoundException());
            }

            var provider = BaseItemProviderFactory.Create(database.Object);

            if (exists)
            {
                var actual = await provider.ReadAsync(id);
                Assert.Equal(
                    id,
                    actual.Id);
            }
            else
            {
                await Assert.ThrowsAsync<NotFoundException>(() => provider.ReadAsync(id));
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ReadAsync_UsingIds(bool exists)
        {
            var database = new Mock<IBaseItemDatabase>();
            var ids = new[]
            {
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            };
            if (exists)
            {
                var baseItemMocks = ids.Select(
                    id =>
                    {
                        var mock = new Mock<IBaseItem>();
                        mock.Setup(x => x.Id).Returns(id);
                        return mock;
                    });

                database.Setup(x => x.ReadAsync(ids)).ReturnsAsync(baseItemMocks.Select(mock => mock.Object));
            }
            else
            {
                database.Setup(x => x.ReadAsync(It.IsAny<IEnumerable<string>>())).ThrowsAsync(new NotFoundException());
            }

            var provider = BaseItemProviderFactory.Create(database.Object);

            if (exists)
            {
                var result = await provider.ReadAsync(ids);
                Assert.Collection(
                    result,
                    actual => Assert.Equal(
                        ids[0],
                        actual.Id),
                    actual => Assert.Equal(
                        ids[1],
                        actual.Id));
            }
            else
            {
                await Assert.ThrowsAsync<NotFoundException>(() => provider.ReadAsync(ids));
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UpdateAsync(bool exists)
        {
            var database = new Mock<IBaseItemDatabase>();
            var id = Guid.NewGuid().ToString();
            var baseItemMock = new Mock<IBaseItem>();
            if (exists)
            {
                baseItemMock.Setup(x => x.Id).Returns(id);
                database.Setup(x => x.UpdateAsync(baseItemMock.Object)).Returns(Task.CompletedTask);
            }
            else
            {
                database.Setup(x => x.UpdateAsync(It.IsAny<IBaseItem>())).ThrowsAsync(new NotFoundException());
            }

            var provider = BaseItemProviderFactory.Create(database.Object);

            if (exists)
            {
                await provider.UpdateAsync(baseItemMock.Object);
            }
            else
            {
                await Assert.ThrowsAsync<NotFoundException>(() => provider.UpdateAsync(baseItemMock.Object));
            }
        }
    }
}
