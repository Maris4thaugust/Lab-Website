using Microsoft.EntityFrameworkCore;
using WebSEnR.Data;
using WebSEnR.Interface.NewsInterface;
using WebSEnR.Models;

namespace WebSEnR.Repository.NewsRepository
{
    public class NewsRepository : INewsRepository
    {
        private readonly SErNDBContext _context;
        public NewsRepository(SErNDBContext context)
        {
            _context = context;
        }
        public bool Add(News News)
        {
            _context.Add(News);
            return Save();
        }

        public bool Delete(News News)
        {
            _context.Remove(News);
            return Save();
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.News.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<News> GetByIdAsyncNoTracking(int id)
        {
            return await _context.News.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(News News)
        {
            _context.Update(News);
            return Save();
        }
    }
}
