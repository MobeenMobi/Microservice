using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace product_service.Models
{
    [Table("Product", Schema = "dbo")]
    public class Product
    {
        [Key]
        public int Id { get; set; }   // Primary Key
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
