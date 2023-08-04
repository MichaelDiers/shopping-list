namespace ShoppingList.BaseItems.Contracts.Providers
{
    using ShoppingList.BaseItems.Contracts.Models;
    using ShoppingList.Shared.Exceptions;

    /// <summary>
    ///     Describes CRUD operations on <see cref="IBaseItem" />s.
    /// </summary>
    public interface IBaseItemDatabase
    {
        /// <summary>
        ///     Clears all items of the database.
        /// </summary>
        /// <returns>A <see cref="Task" />.</returns>
        Task ClearAsync();

        /// <summary>
        ///     Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="AlreadyExistsException">
        ///     Is thrown if the <see cref="IBaseItem.Id" /> already exists in the
        ///     database.
        /// </exception>
        Task CreateAsync(IBaseItem item);

        /// <summary>
        ///     Deletes the item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="NotFoundException">Is thrown if no item with the specified identifier exists.</exception>
        Task DeleteAsync(string id);

        /// <summary>
        ///     Reads the item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task" /> whose result is the item if it exists.</returns>
        /// <exception cref="NotFoundException">Is thrown if no item with the specified id does not exists.</exception>
        Task<IBaseItem> ReadAsync(string id);

        /// <summary>
        ///     Reads all items from the database.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task{T}" /> whose result is an <see cref="IEnumerable{T}" /> of <see cref="IBaseItem" />
        ///     containing all available items.
        /// </returns>
        Task<IEnumerable<IBaseItem>> ReadAsync();

        /// <summary>
        ///     Reads all items with the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>A <see cref="Task" /> whose result contains the items with the specified id.</returns>
        /// <exception cref="NotFoundException">Is thrown if any id has no matching <see cref="IBaseItem" />.</exception>
        Task<IEnumerable<IBaseItem>> ReadAsync(IEnumerable<string> ids);

        /// <summary>
        ///     Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="NotFoundException">Is throw if no item with <see cref="IBaseItem.Id" /> exists.</exception>
        Task UpdateAsync(IBaseItem item);
    }
}
