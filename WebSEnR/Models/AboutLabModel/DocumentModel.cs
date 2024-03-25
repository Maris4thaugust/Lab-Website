using System.ComponentModel.DataAnnotations;
using WebSEnR.Data.Enum;

namespace WebSEnR.Models.AboutLabModel
{
    public class DocumentModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
    }
}
