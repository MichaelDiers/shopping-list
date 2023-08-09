namespace ShoppingList.BaseItems.Requests
{
    using ShoppingList.BaseItems.Models;

    /// <summary>
    ///     Describes a request for creating a new <see cref="BaseItem" />.
    /// </summary>
    public class CreateRequest
    {
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

        /// <summary>
        ///     Gets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public string UserId { get; set; } = "";
    }
}
