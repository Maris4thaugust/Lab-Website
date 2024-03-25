using WebSEnR.Models;

namespace WebSEnR.Interface.NewsInterface
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAll();
        Task<News> GetByIdAsync(int id);
        Task<News> GetByIdAsyncNoTracking(int id);
        bool Add(News Act);
        bool Delete(News Act);
        bool Update(News Act);
        bool Save();
    }
}
