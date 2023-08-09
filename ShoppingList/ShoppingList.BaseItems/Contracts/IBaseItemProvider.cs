namespace ShoppingList.BaseItems.Contracts
{
    using ShoppingList.BaseItems.Models;
    using ShoppingList.BaseItems.Requests;

    /// <summary>
    ///     Describes a provider for base items.
    /// </summary>
    public interface IBaseItemProvider
    {
        /// <summary>
        ///     Creates a new <see cref="BaseItem" /> from the <paramref name="request" /> data.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task" /> whose result is the created <see cref="BaseItem" />.</returns>
        Task<BaseItem> Create(CreateRequest request);

        /// <summary>
        ///     Deletes the item specified by the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task" /> whose result indicates success.</returns>
        Task Delete(string id);

        /// <summary>
        ///     Reads all items.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result is a <see cref="IEnumerable{T}" /> of <see cref="BaseItem" />.</returns>
        Task<IEnumerable<BaseItem>> Read();

        /// <summary>
        ///     Reads the item with the given id.
        /// </summary>
        /// <param name="id">The identifier of the item.</param>
        /// <returns>A <see cref="Task" /> whose result is the requested <see cref="BaseItem" />.</returns>
        Task<BaseItem> Read(string id);

        /// <summary>
        ///     Updates the specified base item.
        /// </summary>
        /// <param name="baseItem">The base item.</param>
        /// <returns>A <see cref="Task" /> that indicates success.</returns>
        Task Update(BaseItem baseItem);
    }
}
