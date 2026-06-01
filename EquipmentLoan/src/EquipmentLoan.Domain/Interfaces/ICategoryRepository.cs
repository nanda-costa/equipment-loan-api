using EquipmentLoan.Domain.Entities;

namespace EquipmentLoan.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category?> GetByIdAsync(Guid id);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task AddAsync(Category category);
        public void Update(Category category);
        public void Delete(Category category);
        public Task<bool> SaveChangesAsync();
    }
}