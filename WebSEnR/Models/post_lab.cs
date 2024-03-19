using System.ComponentModel.DataAnnotations;

namespace WebSEnR.Models
{
    public class post_lab
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string content { get; set; }
        public string datetime { get; set; }
    }
}
