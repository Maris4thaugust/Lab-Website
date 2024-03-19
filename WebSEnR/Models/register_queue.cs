using System.ComponentModel.DataAnnotations;

namespace WebSEnR.Models
{
    public class register_queue
    {
        [Key]
        public int id { get; set; }
        public string fname { get; set; }
        public string dob { get; set; }
        public string school { get; set; }            
        public string faculty { get; set; }            
        public string grade { get; set; }            
        public string desc_abtme { get; set; }
        public string desc_abt_wanna { get; set; }
        public string cv_link { get; set; }
        public string sdt { get; set; }
    }
}
