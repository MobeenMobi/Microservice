using graphql_gateway.DTO;
using System.Net.Http.Json;

namespace graphql_gateway.GraphQL
{
    public class Query
    {
        private readonly IHttpClientFactory _http;

        // Inject IHttpClientFactory
        public Query(IHttpClientFactory http)
        {
            _http = http;
        }

        // Simple test query
        public string TestOrders()
        {
            return "GraphQL is working!";
        }

        // Async query to fetch order with product details
        [GraphQLName("getOrderWithProduct")]
        public async Task<OrderProductResponse> GetOrderWithProduct(int orderId)
        {
            var ordersApi = _http.CreateClient("Orders");
            var productsApi = _http.CreateClient("Products");

            var order = await ordersApi.GetFromJsonAsync<OrderDto>($"api/orders/{orderId}");
            if (order == null)
                return null;

            var product = await productsApi.GetFromJsonAsync<ProductDto>($"api/products/{order.ProductId}");
            if (product == null)
                return null;

            return new OrderProductResponse
            {
                Id = order.Id,
                Quantity = order.Quantity,
                ProductName = product.Name,
                ProductPrice = product.Price
            };
        }
    }
}
