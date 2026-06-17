namespace ContractManagement.API.Models;

public class LoginModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}

public class UpdateStatusRequest
{
    public ContractStatus Status { get; set; }
}

public class UpdateServiceRequestStatus
{
    public RequestStatus Status { get; set; }
}