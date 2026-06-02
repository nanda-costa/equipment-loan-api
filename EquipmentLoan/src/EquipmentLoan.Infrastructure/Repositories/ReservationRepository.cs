using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;
using EquipmentLoan.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace EquipmentLoan.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _context.Reservations
            .Include(r => r.Equipment)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .Include(r => r.Equipment)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<bool> HasOverlappingReservationAsync(Guid equipmentId, DateTime start, DateTime end)
    {
        return await _context.Reservations
            .AnyAsync(r => r.EquipmentId == equipmentId &&
                           r.IsActive &&
                           start < r.ExpirationDate && end > r.ReservedForDate);
    }

    public async Task AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
    }

    public void Update(Reservation reservation)
    {
        _context.Reservations.Update(reservation);
    }

    public void Delete(Reservation reservation)
    {
        _context.Reservations.Remove(reservation);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}