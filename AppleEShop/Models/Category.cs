using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppleEShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [System.ComponentModel.DisplayName("Tên danh mục")]
        public string? Name { get; set; }

        [NotMapped]
        public int ProductCount { get; set; }
    }
}
