namespace ShoppingList.BaseItems.Providers
{
    using ShoppingList.BaseItems.Contracts.Models;
    using ShoppingList.BaseItems.Contracts.Providers;
    using ShoppingList.BaseItems.Models;
    using ShoppingList.BaseItems.Requests;
    using ShoppingList.Shared.Exceptions;

    /// <summary>
    ///     A provider for <see cref="BaseItem" />s.
    /// </summary>
    internal class BaseItemProvider : IBaseItemProvider
    {
        private readonly IBaseItemDatabase database;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseItemProvider" /> class.
        /// </summary>
        /// <param name="database">The database for accessing <see cref="BaseItem" />s.</param>
        public BaseItemProvider(IBaseItemDatabase database)
        {
            this.database = database;
        }

        /// <summary>
        ///     Clears all items of the provider.
        /// </summary>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task ClearAsync()
        {
            await this.database.ClearAsync();
        }

        /// <summary>
        ///     Creates a new <see cref="BaseItem" />.
        /// </summary>
        /// <param name="request">The basic <see cref="BaseItem" /> information.</param>
        /// <returns>The created <see cref="BaseItem" />.</returns>
        /// <exception cref="AlreadyExistsException">
        ///     Is thrown if the <see cref="IBaseItem.Id" /> already exists in the
        ///     database.
        /// </exception>
        public async Task<IBaseItem> CreateAsync(CreateRequest request)
        {
            var baseItem = new BaseItem
            {
                Id = Guid.NewGuid().ToString(),
                MinRequiredQuantityInStock = request.MinRequiredQuantityInStock,
                Name = request.Name
            };

            await this.database.CreateAsync(baseItem);
            return baseItem;
        }

        /// <summary>
        ///     Deletes the item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="NotFoundException">Is thrown if no item with the specified identifier exists.</exception>
        public async Task DeleteAsync(string id)
        {
            await this.database.DeleteAsync(id);
        }

        /// <summary>
        ///     Reads the item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task" /> whose result is the item if it exists.</returns>
        /// <exception cref="NotFoundException">Is thrown if no item with the specified id does not exists.</exception>
        public async Task<IBaseItem> ReadAsync(string id)
        {
            return await this.database.ReadAsync(id);
        }

        /// <summary>
        ///     Reads all items.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task{T}" /> whose result is an <see cref="IEnumerable{T}" /> of <see cref="IBaseItem" />
        ///     containing all available items.
        /// </returns>
        public async Task<IEnumerable<IBaseItem>> ReadAsync()
        {
            return await this.database.ReadAsync();
        }

        /// <summary>
        ///     Reads all items with the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>A <see cref="Task" /> whose result contains the items with the specified id.</returns>
        /// <exception cref="NotFoundException">Is thrown if any id has no matching <see cref="IBaseItem" />.</exception>
        public async Task<IEnumerable<IBaseItem>> ReadAsync(IEnumerable<string> ids)
        {
            return await this.database.ReadAsync(ids);
        }

        /// <summary>
        ///     Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="NotFoundException">Is throw if no item with <see cref="IBaseItem.Id" /> exists.</exception>
        public async Task UpdateAsync(IBaseItem item)
        {
            await this.database.UpdateAsync(item);
        }
    }
}
