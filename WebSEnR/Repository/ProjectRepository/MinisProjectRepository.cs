using Microsoft.EntityFrameworkCore;
using WebSEnR.Data;
using WebSEnR.Interface.ProjectInterface;
using WebSEnR.Models.ProjectsModel;

namespace WebSEnR.Repository.ProjectRepository
{

    public class MinisProjectRepository : IMinisProjectRepository
    {
        private readonly SErNDBContext _context;
        public MinisProjectRepository(SErNDBContext context) 
        {
            _context = context;
        }
        public bool Add(MinisProject Mpjt)
        {
            _context.Add(Mpjt);
            return Save();
        }

        public bool Delete(MinisProject Mpjt)
        {
            _context.Remove(Mpjt);
            return Save();
        }
        public async Task<IEnumerable<MinisProject>> GetAll()
        {
            return await _context.MinisProjects.ToListAsync();
        }

        public async Task<MinisProject> GetByIdAsync(int id)
        {
            return await _context.MinisProjects.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<MinisProject> GetByIdAsyncNoTracking(int id)
        {
            return await _context.MinisProjects.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;    
        }

        public bool Update(MinisProject Mpjt)
        {
            _context.Update(Mpjt);
            return Save();
        }
    }
}
