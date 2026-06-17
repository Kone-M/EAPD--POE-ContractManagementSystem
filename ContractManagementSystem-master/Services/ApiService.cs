
using System.Text;
using System.Text.Json;

namespace ContractManagementSystem.Services;

public interface IApiService
{
    Task<T?> GetAsync<T>(string endpoint);
    Task<T?> PostAsync<T>(string endpoint, object data);
    Task<T?> PutAsync<T>(string endpoint, object data);
    Task<bool> PatchAsync(string endpoint, object data);
    Task<bool> DeleteAsync(string endpoint);
    Task<bool> LoginAsync(string username, string password);
    void SetAuthToken(string token);
    void ClearAuthToken();
    bool IsAuthenticated { get; }
}

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string? _authToken;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _jsonOptions = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
        };
        
        var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
        if (!string.IsNullOrEmpty(token))
        {
            SetAuthToken(token);
        }
    }

    public bool IsAuthenticated => !string.IsNullOrEmpty(_authToken);

public void SetAuthToken(string token)
{
    _authToken = token;
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    _httpContextAccessor.HttpContext?.Session.SetString("AuthToken", token);
}

public void ClearAuthToken()
{
    _authToken = null;
    _httpClient.DefaultRequestHeaders.Authorization = null;
    _httpContextAccessor.HttpContext?.Session.Remove("AuthToken");
}

public async Task<T?> GetAsync<T>(string endpoint)
{
    try
    {
        var response = await _httpClient.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, _jsonOptions);
        }
        return default;
    }
    catch
    {
        return default;
    }
}

public async Task<T?> PostAsync<T>(string endpoint, object data)
{
    try
    {
        var json = JsonSerializer.Serialize(data, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        }
        return default;
    }
    catch
    {
        return default;
    }
}

public async Task<T?> PutAsync<T>(string endpoint, object data)
{
    try
    {
        var json = JsonSerializer.Serialize(data, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        }
        return default;
    }
    catch
    {
        return default;
    }
}

public async Task<bool> PatchAsync(string endpoint, object data)
{
    try
    {
        var json = JsonSerializer.Serialize(data, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PatchAsync(endpoint, content);
        return response.IsSuccessStatusCode;
    }
    catch
    {
        return false;
    }
}

public async Task<bool> DeleteAsync(string endpoint)
{
    try
    {
        var response = await _httpClient.DeleteAsync(endpoint);
        return response.IsSuccessStatusCode;
    }
    catch
    {
        return false;
    }
}

public async Task<bool> LoginAsync(string username, string password)
{
    try
    {
        var loginData = new { username, password };
        var json = JsonSerializer.Serialize(loginData, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponse>(responseContent, _jsonOptions);
            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                SetAuthToken(result.Token);
                return true;
            }
        }
        return false;
    }
    catch
    {
        return false;
    }
}

private class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
}