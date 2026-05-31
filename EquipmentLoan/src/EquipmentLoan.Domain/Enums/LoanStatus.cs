namespace EquipmentLoan.Domain.Enums;

public enum LoanStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    ReturnedNormal = 4,
    ReturnedLate = 5,
    ReturnedDamaged = 6,
}