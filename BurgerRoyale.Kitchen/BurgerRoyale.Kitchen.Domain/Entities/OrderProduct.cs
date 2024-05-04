namespace BurgerRoyale.Kitchen.Domain.Entities
{
    public class OrderProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
