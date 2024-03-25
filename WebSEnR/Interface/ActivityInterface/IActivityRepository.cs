using WebSEnR.Models;
using WebSEnR.Models.AboutLabModel;

namespace WebSEnR.Interface.ActivityInterface
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetAll();
        Task<Activity> GetByIdAsync(int id);
        Task<Activity> GetByIdAsyncNoTracking(int id);
        bool Add(Activity Act);
        bool Delete(Activity Act);
        bool Update(Activity Act);
        bool Save();
    }
}
