using Microsoft.EntityFrameworkCore;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;
using EquipmentLoan.Infrastructure.DbContext;

namespace EquipmentLoan.Infrastructure.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _context;

        public EquipmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Equipment?> GetByIdAsync(Guid id)
        {
            return await _context.Equipments
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Equipment>> GetByFilterAsync(EquipmentStatus? status, Guid? categoryId)
        {
            IQueryable<Equipment> query = _context.Equipments.Include(e => e.Category);

            if (status.HasValue)
            {
                query = query.Where(e => e.Status == status.Value);
            }

            if (categoryId.HasValue)
            {
                query = query.Where(e => e.CategoryId == categoryId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(Equipment equipment)
        {
            await _context.Equipments.AddAsync(equipment);
        }

        public void Update(Equipment equipment)
        {
            _context.Equipments.Update(equipment);
        }

        public void Delete(Equipment equipment)
        {
            _context.Equipments.Remove(equipment);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}