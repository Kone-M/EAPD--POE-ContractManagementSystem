using Microsoft.AspNetCore.Mvc;
using ContractManagement.API.Models;
using ContractManagement.API.Services;

namespace ContractManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            return BadRequest(new { message = "Username and password are required" });

        var token = await _authService.AuthenticateAsync(model.Username, model.Password);
        if (token == null)
            return Unauthorized(new { message = "Invalid username or password" });

        var user = await _authService.GetUserByUsernameAsync(model.Username);

        return Ok(new AuthResponse
        {
            Token = token,
            Username = model.Username,
            Role = user?.Role ?? "User",
            ExpiresAt = DateTime.UtcNow.AddHours(8)
        });
    }
}