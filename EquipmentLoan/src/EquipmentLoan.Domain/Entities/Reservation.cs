using System;

namespace EquipmentLoan.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime ReservedForDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; } = true;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;
    }
}