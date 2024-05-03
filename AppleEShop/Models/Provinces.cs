using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppleEShop.Models
{
    [Table("Provinces")]
    public class Provinces
    {
        [Key]
        public int province_id { get; set; }
        public string province_name { get; set; }
        public string type { get; set; }
    }
}
