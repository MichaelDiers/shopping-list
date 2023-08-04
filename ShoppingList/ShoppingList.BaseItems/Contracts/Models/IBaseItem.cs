namespace ShoppingList.BaseItems.Contracts.Models
{
    /// <summary>
    ///     Describes an <see cref="IBaseItem" />.
    /// </summary>
    public interface IBaseItem
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        string Id { get; set; }

        /// <summary>
        ///     Gets or sets the minimum required quantity in stock.
        /// </summary>
        /// <value>
        ///     The minimum required quantity in stock.
        /// </value>
        int MinRequiredQuantityInStock { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }
    }
}
