using Microsoft.EntityFrameworkCore;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;
using EquipmentLoan.Infrastructure.DbContext;

namespace EquipmentLoan.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
            // O EF Core monitora a entidade automaticamente, mas deixar explicito
            // ajuda na legibilidade e no padrão de repositórios
            _context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Retorna verdadeiro se pelo menos uma linha foi afetada no banco
            return await _context.SaveChangesAsync() > 0;
        }
    }
}