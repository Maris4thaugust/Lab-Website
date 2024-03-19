using WebSEnR.Models.AboutLabModel;
using WebSEnR.Models.ProjectsModel;

namespace WebSEnR.Interface.AboutLabInterface
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipments>> GetAll();
        Task<Equipments> GetByIdAsync(int id);
        Task<Equipments> GetByIdAsyncNoTracking(int id);
        bool Add(Equipments Eqmt);
        bool Delete(Equipments Eqmt);
        bool Update(Equipments Eqmt);
        bool Save();
    }
}
