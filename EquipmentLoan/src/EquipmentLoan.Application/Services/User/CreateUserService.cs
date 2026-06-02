using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces.User;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services;

public class CreateUserService : ICreateUser
{
    private readonly IUserRepository _userRepository;

    public CreateUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto> Execute(UserCreateRequestDto requestDto)
    {
        User existingUser = await _userRepository.GetByEmailAsync(requestDto.Email);
        if (existingUser != null)
        {
            throw new Exception("Este e-mail já está em uso no sistema.");
        }

        User user = new User
        {
            Name = requestDto.Name,
            Email = requestDto.Email,
            PasswordHash = requestDto.Password, // 🔒 O Membro 2 vai mudar isso aqui para _hashService.Hash(password)
            Role = requestDto.Role
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }
}