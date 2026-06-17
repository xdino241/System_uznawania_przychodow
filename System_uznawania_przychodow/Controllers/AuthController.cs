using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System_uznawania_przychodow.DTOs.Auth;
using System_uznawania_przychodow.Services.AuthService;

namespace System_uznawania_przychodow.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
 
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
 
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }
}