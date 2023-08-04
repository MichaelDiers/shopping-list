namespace ShoppingList.BaseItems.Providers
{
    using ShoppingList.BaseItems.Contracts.Models;
    using ShoppingList.BaseItems.Contracts.Providers;
    using ShoppingList.Shared.Exceptions;

    /// <summary>
    ///     A database for <see cref="IBaseItem" />s.
    /// </summary>
    /// <seealso cref="ShoppingList.BaseItems.Contracts.Providers.IBaseItemDatabase" />
    internal class BaseItemDatabase : IBaseItemDatabase
    {
        private readonly IDictionary<string, IBaseItem> database = new Dictionary<string, IBaseItem>();

        /// <summary>
        ///     Clears all items of the database.
        /// </summary>
        /// <returns>A <see cref="Task" />.</returns>
        public Task ClearAsync()
        {
            this.database.Clear();
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="AlreadyExistsException">
        ///     Is thrown if the <see cref="IBaseItem.Id" /> already exists in the
        ///     database.
        /// </exception>
        public Task CreateAsync(IBaseItem item)
        {
            if (this.database.ContainsKey(item.Id))
            {
                throw new AlreadyExistsException();
            }

            this.database[item.Id] = item;
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Deletes the item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="NotFoundException">Is thrown if no item with the specified identifier exists.</exception>
        public Task DeleteAsync(string id)
        {
            if (!this.database.Remove(id))
            {
                throw new NotFoundException();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Reads all items from the database.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task{T}" /> whose result is an <see cref="IEnumerable{T}" /> of <see cref="IBaseItem" />
        ///     containing all available items.
        /// </returns>
        public Task<IEnumerable<IBaseItem>> ReadAsync()
        {
            return Task.FromResult(this.database.Values as IEnumerable<IBaseItem>);
        }

        /// <summary>
        ///     Reads the item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task" /> whose result is the item if it exists.</returns>
        /// <exception cref="NotFoundException">Is thrown if no item with the specified id does not exists.</exception>
        public Task<IBaseItem> ReadAsync(string id)
        {
            if (this.database.TryGetValue(
                    id,
                    out var item))
            {
                return Task.FromResult(item);
            }

            throw new NotFoundException();
        }

        /// <summary>
        ///     Reads all items with the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>A <see cref="Task" /> whose result contains the items with the specified id.</returns>
        /// <exception cref="NotFoundException">Is thrown if any id has no matching <see cref="IBaseItem" />.</exception>
        public Task<IEnumerable<IBaseItem>> ReadAsync(IEnumerable<string> ids)
        {
            var tasks = ids.Select(this.ReadAsync).ToArray();
            Task.WaitAll(tasks);
            return Task.FromResult(tasks.Select(task => task.Result));
        }

        /// <summary>
        ///     Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="NotFoundException">Is throw if no item with <see cref="IBaseItem.Id" /> exists.</exception>
        public Task UpdateAsync(IBaseItem item)
        {
            if (!this.database.ContainsKey(item.Id))
            {
                throw new NotFoundException();
            }

            this.database[item.Id] = item;
            return Task.CompletedTask;
        }
    }
}
