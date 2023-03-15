namespace DataContext.Models

{
    public class Order
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public int CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
