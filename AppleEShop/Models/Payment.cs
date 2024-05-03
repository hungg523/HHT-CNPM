using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppleEShop.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [System.ComponentModel.DisplayName("Phương thức nhận hàng")]
        public string? Name { get; set; }
        [Required]
        [MaxLength(500)]
        [System.ComponentModel.DisplayName("Mô tả")]
        public string? Description { get; set; }
    }
}
