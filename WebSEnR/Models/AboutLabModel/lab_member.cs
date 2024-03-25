using System.ComponentModel.DataAnnotations;

namespace WebSEnR.Models.AboutLabModel
{
    public class lab_member
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string school { get; set; }
        public string facul { get; set; }
        public string grade { get; set; }
        public string role { get; set; }
        public string desc { get; set; }
        public string joined_prjs { get; set; }
    }
}
