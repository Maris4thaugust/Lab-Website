using WebSEnR.Data.Enum;
using WebSEnR.Models;

namespace WebSEnR.ViewModel.NewsViewModel
{
    public class CreateNewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? Url { get; set; }
        public NewsCategory NewsCategory { get; set; }
    }
}
