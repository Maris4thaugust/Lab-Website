using WebSEnR.Models.ProjectsModel;

namespace WebSEnR.Interface.ProjectInterface
{

    public interface IMinisProjectRepository
    {
        Task<IEnumerable<MinisProject>> GetAll();
        Task<MinisProject> GetByIdAsync(int id);
        Task<MinisProject> GetByIdAsyncNoTracking(int id);
        bool Add(MinisProject Mpjt);
        bool Delete(MinisProject Mpjt);
        bool Update(MinisProject Mpjt);
        bool Save();
    }
}
