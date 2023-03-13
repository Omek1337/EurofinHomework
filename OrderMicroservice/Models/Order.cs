namespace OrderMicroservice.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
