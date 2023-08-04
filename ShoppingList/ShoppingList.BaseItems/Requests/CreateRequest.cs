namespace ShoppingList.BaseItems.Requests
{
    public class CreateRequest
    {
        public int MinRequiredQuantityInStock { get; set; }
        public string Name { get; set; } = "";
    }
}
