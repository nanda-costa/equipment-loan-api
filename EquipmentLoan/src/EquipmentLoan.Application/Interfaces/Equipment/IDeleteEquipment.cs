
namespace EquipmentLoan.Application.Interfaces;
public interface IDeleteEquipment
{
    public Task<bool> Execute(Guid id);
}