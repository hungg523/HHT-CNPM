using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppleEShop.Models
{
    [Table("Districts")]
    public class Districts
    {
        [Key]
        public int district_id { get; set; }
        public int province_id { get; set; }
        public string district_name { get; set; }
        public string type { get; set; }
    }
}
