using WebSEnR.Data.Enum;

namespace WebSEnR.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Url { get; set; }
        public string? Image { get; set; }
        public NewsCategory NewsCategory { get; set; }
    }
}
