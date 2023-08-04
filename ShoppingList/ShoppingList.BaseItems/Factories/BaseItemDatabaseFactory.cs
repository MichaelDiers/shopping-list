namespace ShoppingList.BaseItems.Factories
{
    using ShoppingList.BaseItems.Contracts.Providers;
    using ShoppingList.BaseItems.Providers;

    /// <summary>
    ///     Factory for <see cref="IBaseItemDatabase" />.
    /// </summary>
    public static class BaseItemDatabaseFactory
    {
        /// <summary>
        ///     Factory method for <see cref="IBaseItemDatabase" />.
        /// </summary>
        /// <returns>An <see cref="IBaseItemDatabase" />.</returns>
        public static IBaseItemDatabase Create()
        {
            return new BaseItemDatabase();
        }
    }
}
