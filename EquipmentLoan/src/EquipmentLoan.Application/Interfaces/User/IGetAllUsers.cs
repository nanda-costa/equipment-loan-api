using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces.User;

public interface IGetAllUsers
{
    public Task<IEnumerable<UserResponseDto>> Execute();
}