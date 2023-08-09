namespace ShoppingList.BaseItems.Providers
{
    using ShoppingList.BaseItems.Contracts;
    using ShoppingList.BaseItems.Models;
    using ShoppingList.BaseItems.Requests;

    /// <summary>
    ///     A provider for base items.
    /// </summary>
    public class BaseItemProvider : IBaseItemProvider
    {
        /// <summary>
        ///     Creates a new <see cref="BaseItem" /> from the <paramref name="request" /> data.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task" /> whose result is the created <see cref="BaseItem" />.</returns>
        public Task<BaseItem> Create(CreateRequest request)
        {
            var baseItem = new BaseItem(
                Guid.NewGuid().ToString(),
                request.Name,
                request.MinRequiredQuantityInStock,
                request.UserId);
            return Task.FromResult(baseItem);
        }

        /// <summary>
        ///     Deletes the item specified by the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task" /> whose result indicates success.</returns>
        public Task Delete(string id)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Reads all items.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result is a <see cref="IEnumerable{T}" /> of <see cref="BaseItem" />.</returns>
        public Task<IEnumerable<BaseItem>> Read()
        {
            var baseItems = Enumerable.Range(
                    0,
                    10)
                .Select(
                    i => new BaseItem(
                        Guid.NewGuid().ToString(),
                        $"Name_{i}",
                        i,
                        Guid.NewGuid().ToString()));
            return Task.FromResult(baseItems);
        }

        /// <summary>
        ///     Reads the item with the given id.
        /// </summary>
        /// <param name="id">The identifier of the item.</param>
        /// <returns>A <see cref="Task" /> whose result is the requested <see cref="BaseItem" />.</returns>
        public Task<BaseItem> Read(string id)
        {
            var baseItem = new BaseItem(
                id,
                "Name",
                10,
                Guid.NewGuid().ToString());
            return Task.FromResult(baseItem);
        }

        /// <summary>
        ///     Updates the specified base item.
        /// </summary>
        /// <param name="baseItem">The base item.</param>
        /// <returns>A <see cref="Task" /> that indicates success.</returns>
        public Task Update(BaseItem baseItem)
        {
            return Task.CompletedTask;
        }
    }
}
