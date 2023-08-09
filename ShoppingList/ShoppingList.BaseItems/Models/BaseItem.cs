namespace ShoppingList.BaseItems.Models
{
    /// <summary>
    ///     Describes a <see cref="BaseItem" />.
    /// </summary>
    public class BaseItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseItem" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="minRequiredQuantityInStock">The minimum required quantity in stock.</param>
        /// <param name="userId">The user identifier.</param>
        public BaseItem(
            string id,
            string name,
            int minRequiredQuantityInStock,
            string userId
        )
        {
            this.Id = id;
            this.Name = name;
            this.MinRequiredQuantityInStock = minRequiredQuantityInStock;
            this.UserId = userId;
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        ///     Gets the minimum required quantity in stock.
        /// </summary>
        /// <value>
        ///     The minimum required quantity in stock.
        /// </value>
        public int MinRequiredQuantityInStock { get; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        ///     Gets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public string UserId { get; }
    }
}
