using Microsoft.EntityFrameworkCore;
using WebSEnR.Data;
using WebSEnR.Interface.AboutLabInterface;
using WebSEnR.Models.AboutLabModel;

namespace WebSEnR.Repository.AboutLabRepository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly SErNDBContext _context;
        public EquipmentRepository(SErNDBContext context)
        {
            _context = context;
        }
        public bool Add(Equipments Eqmt)
        {
            _context.Add(Eqmt);
            return Save();
        }

        public bool Delete(Equipments Eqmt)
        {
            _context.Remove(Eqmt);
            return Save();
        }

        public async Task<IEnumerable<Equipments>> GetAll()
        {
            return await _context.Equipments.ToListAsync();
        }

        public async Task<Equipments> GetByIdAsync(int id)
        {
            return await _context.Equipments.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Equipments> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Equipments.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Equipments Eqmt)
        {
            _context.Update(Eqmt);
            return Save();
        }
    }
}
