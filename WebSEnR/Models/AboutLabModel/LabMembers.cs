using System.ComponentModel.DataAnnotations;
using WebSEnR.Data.Enum;

namespace WebSEnR.Models.AboutLabModel
{
    public class LabMembers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string School { get; set; }
        public string Major { get; set; }
        public string Grade { get; set; }
        public string Project { get; set; }
        public string MSSV { get; set; }
    }
}
