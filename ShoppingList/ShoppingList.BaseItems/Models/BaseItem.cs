namespace ShoppingList.BaseItems.Models
{
    using ShoppingList.BaseItems.Contracts.Models;

    /// <summary>
    ///     Describes a <see cref="BaseItem" />.
    /// </summary>
    /// <seealso cref="ShoppingList.BaseItems.Contracts.Models.IBaseItem" />
    public class BaseItem : IBaseItem
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public string Id { get; set; } = "";

        /// <summary>
        ///     Gets or sets the minimum required quantity in stock.
        /// </summary>
        /// <value>
        ///     The minimum required quantity in stock.
        /// </value>
        public int MinRequiredQuantityInStock { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; } = "";
    }
}
