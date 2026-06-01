using Microsoft.EntityFrameworkCore;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;
using EquipmentLoan.Infrastructure.DbContext;

namespace EquipmentLoan.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;

        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await _context.Loans
                .Include(l => l.User)
                .Include(l => l.Equipment)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Loan>> GetByUserAsync(Guid userId)
        {
            return await _context.Loans
                .Include(l => l.User)
                .Include(l => l.Equipment)
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.RequestDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetByFilterAsync(
            LoanStatus? status,
            Guid? userId,
            Guid? equipmentId,
            DateTime? startDate,
            DateTime? endDate)
        {
            IQueryable<Loan> query = _context.Loans
                .Include(l => l.User)
                .Include(l => l.Equipment);

            if (status.HasValue)
            {
                query = query.Where(l => l.Status == status.Value);
            }

            if (userId.HasValue)
            {
                query = query.Where(l => l.UserId == userId.Value);
            }

            if (equipmentId.HasValue)
            {
                query = query.Where(l => l.EquipmentId == equipmentId.Value);
            }

            // Filtro por período baseado na data da solicitação.
            if (startDate.HasValue)
            {
                query = query.Where(l => l.RequestDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(l => l.RequestDate <= endDate.Value);
            }

            return await query
                .OrderByDescending(l => l.RequestDate)
                .ToListAsync();
        }

        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }

        public void Update(Loan loan)
        {
            _context.Loans.Update(loan);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
