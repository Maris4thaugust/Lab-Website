using Microsoft.EntityFrameworkCore;
using WebSEnR.Data;
using WebSEnR.Interface.ProjectInterface;
using WebSEnR.Models.ProjectsModel;

namespace WebSEnR.Repository.ProjectRepository
{
    public class UniProjectRepository : IUniProjectRepository
    {
        private readonly SErNDBContext _context;
        public UniProjectRepository(SErNDBContext context)
        {
            _context = context;
        }
        public bool Add(UniProject Upjt)
        {
            _context.Add(Upjt);
            return Save();
        }

        public async Task<IEnumerable<UniProject>> GetAll()
        {
            return await _context.Uniprojects.ToListAsync();
        }

        public async Task<UniProject> GetByIdAsync(int id)
        {
            return await _context.Uniprojects.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<UniProject> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Uniprojects.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
  
        public bool Delete(UniProject Upjt)
        {
            _context.Remove(Upjt);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(UniProject Upjt)
        {
            _context.Update(Upjt);
            return Save();
        }
    }
}
