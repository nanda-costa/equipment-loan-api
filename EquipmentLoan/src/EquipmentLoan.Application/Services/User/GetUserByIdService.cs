using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces.User;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services;

public class GetUserByIdService : IGetUserById
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto?> Execute(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }
}