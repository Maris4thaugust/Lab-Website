using WebSEnR.Data.Enum;

namespace WebSEnR.ViewModel.DocumentViewModel
{
    public class CreateDocumentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
    }
}
