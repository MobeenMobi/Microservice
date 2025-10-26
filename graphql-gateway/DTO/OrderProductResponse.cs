namespace graphql_gateway.DTO
{
    public class OrderProductResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
