using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace order_service.Models
{
    [Table("orders")]
    public class Orders
    {
        [Key]
        public int id { get; set; }   // Primary Key
        public int productid { get; set; }  // Foreign key (for GraphQL later)
        public int quantity { get; set; }
        public decimal totalamount { get; set; }
    }
}
