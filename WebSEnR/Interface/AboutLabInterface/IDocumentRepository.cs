using WebSEnR.Models.AboutLabModel;

namespace WebSEnR.Interface.AboutLabInterface
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<DocumentModel>> GetAll();
        Task<DocumentModel> GetByIdAsync(int id);
        Task<DocumentModel> GetByIdAsyncNoTracking(int id);
        bool Add(DocumentModel Doc);
        bool Delete(DocumentModel Doc);
        bool Update(DocumentModel Doc);
        bool Save();
    }
}
