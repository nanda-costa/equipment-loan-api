namespace EquipmentLoan.Application.Interfaces;

public interface IRejectLoan
{
    public Task<bool> Execute(Guid id);
}
