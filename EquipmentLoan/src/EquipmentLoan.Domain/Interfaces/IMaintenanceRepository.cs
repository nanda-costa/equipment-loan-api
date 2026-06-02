using EquipmentLoan.Domain.Entities;

namespace EquipmentLoan.Domain.Interfaces;

public interface IMaintenanceRepository
{
    public Task<Maintenance?> GetByIdAsync(Guid id);
    public Task<IEnumerable<Maintenance>> GetAllActiveAsync(); 
    public Task AddAsync(Maintenance maintenance);
    public void Update(Maintenance maintenance);
    public Task<bool> SaveChangesAsync();
}