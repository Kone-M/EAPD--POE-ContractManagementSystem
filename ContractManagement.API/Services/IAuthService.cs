using ContractManagement.API.Models;

namespace ContractManagement.API.Services;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(string username, string password);
    Task<User?> GetUserByUsernameAsync(string username);
}