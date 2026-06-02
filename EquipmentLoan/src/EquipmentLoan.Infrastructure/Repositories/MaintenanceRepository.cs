using Microsoft.EntityFrameworkCore;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;
using EquipmentLoan.Infrastructure.DbContext;

namespace EquipmentLoan.Infrastructure.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly AppDbContext _context;

        public MaintenanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Maintenance?> GetByIdAsync(Guid id)
        {
            return await _context.Maintenances
                .Include(m => m.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Maintenance>> GetAllActiveAsync()
        {
            // 🛠️ LINQ CORRIGIDO: Ativo é quem tem EndDate nulo
            return await _context.Maintenances
                .Include(m => m.Equipment)
                .Where(m => m.EndDate == null) 
                .ToListAsync();
        }

        public async Task AddAsync(Maintenance maintenance)
        {
            await _context.Maintenances.AddAsync(maintenance);
        }

        public void Update(Maintenance maintenance)
        {
            _context.Maintenances.Update(maintenance);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}