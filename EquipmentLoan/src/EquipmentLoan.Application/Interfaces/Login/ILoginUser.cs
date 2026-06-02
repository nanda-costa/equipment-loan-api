using EquipmentLoan.Application.Dtos.Login;

namespace EquipmentLoan.Application.Interfaces.Login;

public interface ILoginUser
{
    public Task<LoginResponseDto> Execute(LoginRequestDto requestDto);
}