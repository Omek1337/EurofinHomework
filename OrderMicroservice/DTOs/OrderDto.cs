namespace OrderMicroservice.DTOs
{
    public class OrderDto
    {
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
