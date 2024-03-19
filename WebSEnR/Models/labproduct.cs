using System.ComponentModel.DataAnnotations;

namespace WebSEnR.Models
{
    public class labproduct
    {
        [Key]
        public int id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string member { get; set; }
        public string desc { get; set; }
        public string href { get; set; }

    }
}
