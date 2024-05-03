using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppleEShop.Models
{
    [Table("Wards")]
    public class Wards
    {
        [Key]
        public int ward_id { get; set; }
        public int district_id { get; set; }
        public string ward_name { get; set; }
        public string type { get; set; }
    }
}
