
namespace EquipmentLoan.Domain.Entities
{
    public class Maintenance
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;
    }
}