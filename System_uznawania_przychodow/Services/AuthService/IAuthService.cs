using System_uznawania_przychodow.DTOs.Auth;

namespace System_uznawania_przychodow.Services.AuthService;

public interface IAuthService
{
    Task<TokenResponseDto> LoginAsync(LoginDto dto);
}