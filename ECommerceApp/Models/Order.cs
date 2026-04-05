namespace ECommerceApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }
        public string? CustomerName { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}