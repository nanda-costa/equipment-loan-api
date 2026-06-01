using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.Domain.Interfaces;

public interface ILoanRepository
{
    public Task<Loan?> GetByIdAsync(Guid id);

    public Task<IEnumerable<Loan>> GetByUserAsync(Guid userId);

    public Task<IEnumerable<Loan>> GetByFilterAsync(
        LoanStatus? status,
        Guid? userId,
        Guid? equipmentId,
        DateTime? startDate,
        DateTime? endDate);

    public Task AddAsync(Loan loan);
    public void Update(Loan loan);
    public Task<bool> SaveChangesAsync();
}
