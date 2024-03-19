using System.ComponentModel.DataAnnotations;

namespace WebSEnR.Models
{
    public class post_etp_queue
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string etp_name { get; set; }
        public string type_i { get; set; }
        public string type_f { get; set; }
        public string type_t { get; set; }
        public string type_r { get; set; }
        public string desc_job { get; set; }
        public string desc_etp { get; set; }
        public string contact_sdt { get; set; }
        public string contact_email { get; set; }
        public string exp_date { get; set; }
        public string post_date { get; set; }
    }
}
