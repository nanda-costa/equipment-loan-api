using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces.User;

public interface ICreateUser
{
    public Task<UserResponseDto> Execute(UserCreateRequestDto requestDto);
}