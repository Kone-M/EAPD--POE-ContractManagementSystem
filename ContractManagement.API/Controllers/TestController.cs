using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "API is working!",
            timestamp = DateTime.UtcNow,
            swagger = "http://localhost:5000/swagger"
        });
    }
}