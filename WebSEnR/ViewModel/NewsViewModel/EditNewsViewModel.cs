using WebSEnR.Data.Enum;

namespace WebSEnR.ViewModel.NewsViewModel
{
    public class EditNewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Image { get; set; }

        public NewsCategory NewsCategory { get; set; }
    }
}
