using Microsoft.EntityFrameworkCore;
using WebSEnR.Data;
using WebSEnR.Interface.ActivityInterface;
using WebSEnR.Models;

namespace WebSEnR.Repository.ActivityRepository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly SErNDBContext _context;
        public ActivityRepository(SErNDBContext context)
        {
            _context = context;
        }
        public bool Add(Activity Act)
        {
            _context.Add(Act);
            return Save();
        }

        public bool Delete(Activity Act)
        {
            _context.Remove(Act);
            return Save();
        }

        public async Task<IEnumerable<Activity>> GetAll()
        {
            return await _context.Activities.ToListAsync();
        }

        public async Task<Activity> GetByIdAsync(int id)
        {
            return await _context.Activities.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Activity> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Activities.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Activity Act)
        {
            _context.Update(Act);
            return Save();
        }
    }
}
