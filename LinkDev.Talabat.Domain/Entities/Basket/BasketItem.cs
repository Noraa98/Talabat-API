namespace LinkDev.Talabat.Domain.Entities.Basket
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? PictureUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
    }

}
