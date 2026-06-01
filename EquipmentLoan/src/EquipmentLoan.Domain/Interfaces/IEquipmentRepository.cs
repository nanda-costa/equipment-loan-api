using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.Domain.Interfaces;

public interface IEquipmentRepository
{
    public Task<Equipment?> GetByIdAsync(Guid id);
        
    public Task<IEnumerable<Equipment>> GetByFilterAsync(EquipmentStatus? status, Guid? categoryId);
        
    public Task AddAsync(Equipment equipment);
    public void Update(Equipment equipment);
    public void Delete(Equipment equipment);
    public Task<bool> SaveChangesAsync();
}    