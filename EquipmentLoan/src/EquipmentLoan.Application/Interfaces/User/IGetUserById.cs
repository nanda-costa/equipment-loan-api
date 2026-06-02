using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces.User;

public interface IGetUserById
{
    public Task<UserResponseDto?> Execute(Guid id);
}