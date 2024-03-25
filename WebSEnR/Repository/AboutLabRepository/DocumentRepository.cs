using Microsoft.EntityFrameworkCore;
using WebSEnR.Data;
using WebSEnR.Interface.AboutLabInterface;
using WebSEnR.Models.AboutLabModel;

namespace WebSEnR.Repository.AboutLabRepository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly SErNDBContext _context;

        public DocumentRepository(SErNDBContext context)
        {
            _context = context;
        }
        public bool Add(DocumentModel Doc)
        {
            _context.Add(Doc);
            return Save();
        }
        public bool Delete(DocumentModel Doc)
        {
            _context.Add(Doc);
            return Save();
        }

        public async Task<IEnumerable<DocumentModel>> GetAll()
        {
            return await _context.DocumentModel.ToListAsync();
        }

        public async Task<DocumentModel> GetByIdAsync(int id)
        {
            return await _context.DocumentModel.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<DocumentModel> GetByIdAsyncNoTracking(int id)
        {
            return await _context.DocumentModel.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(DocumentModel Doc)
        {
            _context.Update(Doc);
            return Save();
        }
    }
}
