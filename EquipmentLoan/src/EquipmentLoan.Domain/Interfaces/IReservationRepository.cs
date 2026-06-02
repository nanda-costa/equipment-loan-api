using EquipmentLoan.Domain.Entities;

namespace EquipmentLoan.Domain.Interfaces;

public interface IReservationRepository
{
    public Task<Reservation?> GetByIdAsync(Guid id);
    Task<IEnumerable<Reservation>> GetAllAsync();
        
    public Task<bool> HasOverlappingReservationAsync(Guid equipmentId, DateTime start, DateTime end);
        
    public Task AddAsync(Reservation reservation);
    public void Update(Reservation reservation);
    public void Delete(Reservation reservation);
    public Task<bool> SaveChangesAsync();
}