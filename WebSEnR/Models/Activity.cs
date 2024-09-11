using System.ComponentModel.DataAnnotations;
using WebSEnR.Data.Enum;

namespace WebSEnR.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Url { get; set; }
        public string? Image {  get; set; }
        public ActivityCategory ActivityCategory { get; set; }

    }
}
