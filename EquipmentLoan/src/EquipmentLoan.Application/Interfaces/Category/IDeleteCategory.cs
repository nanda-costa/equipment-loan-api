namespace EquipmentLoan.Application.Interfaces;

public interface IDeleteCategory
{
    public Task<bool> Execute(Guid id);

}