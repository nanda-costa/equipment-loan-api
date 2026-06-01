namespace EquipmentLoan.Application.Interfaces;

public interface IApproveLoan
{
    public Task<bool> Execute(Guid id);
}
