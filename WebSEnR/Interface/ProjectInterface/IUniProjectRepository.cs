using WebSEnR.Models.ProjectsModel;

namespace WebSEnR.Interface.ProjectInterface
{
    public interface IUniProjectRepository
    {
        Task<IEnumerable<UniProject>> GetAll();   /*IEnumerable help that element can be search in foreach*/
        Task<UniProject> GetByIdAsync(int id); /*Create method that we want*/
        Task<UniProject> GetByIdAsyncNoTracking(int id);
        bool Add(UniProject Upjt);
        bool Delete(UniProject Upjt);
        bool Update(UniProject Upjt);
        bool Save();
    }
}
