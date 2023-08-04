namespace ShoppingList.BaseItems.Factories
{
    using ShoppingList.BaseItems.Contracts.Models;
    using ShoppingList.BaseItems.Contracts.Providers;
    using ShoppingList.BaseItems.Providers;

    /// <summary>
    ///     Factory for <see cref="IBaseItemProvider" />.
    /// </summary>
    public static class BaseItemProviderFactory
    {
        /// <summary>
        ///     Factory method for <see cref="IBaseItemProvider" />.
        /// </summary>
        /// <param name="database">Access to the database of <see cref="IBaseItem" />.</param>
        /// <returns>An <see cref="IBaseItemProvider" />.</returns>
        public static IBaseItemProvider Create(IBaseItemDatabase database)
        {
            return new BaseItemProvider(database);
        }
    }
}
