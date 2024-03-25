using WebSEnR.Data.Enum;

namespace WebSEnR.ViewModel.ActivityViewModel
{
    public class CreateActivityViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? Url { get; set; }
        public ActivityCategory ActivityCategory { get; set; }
    }
}
