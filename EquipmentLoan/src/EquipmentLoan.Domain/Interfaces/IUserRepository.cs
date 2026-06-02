using EquipmentLoan.Domain.Entities;

namespace EquipmentLoan.Domain.Interfaces;

public interface IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id);
    public Task<User?> GetByEmailAsync(string email); 
    public Task<IEnumerable<User>> GetAllAsync();
    public Task AddAsync(User user);
    public void Update(User user);
    public void Delete(User user);
    public Task<bool> SaveChangesAsync();
}