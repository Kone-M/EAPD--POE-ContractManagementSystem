using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContractManagement.API.Models;
using ContractManagement.API.Repositories;

namespace ContractManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ServiceRequestsController : ControllerBase
{
    private readonly IContractRepository _repository;

    public ServiceRequestsController(IContractRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("contract/{contractId}")]
    [ProducesResponseType(typeof(IEnumerable<ServiceRequest>), 200)]
    public async Task<IActionResult> GetByContract(int contractId)
    {
        var requests = await _repository.GetServiceRequestsByContractIdAsync(contractId);
        return Ok(requests);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ServiceRequest), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var request = await _repository.GetServiceRequestByIdAsync(id);
        if (request == null)
            return NotFound(new { message = $"Service request with ID {id} not found" });
        return Ok(request);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ServiceRequest), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] ServiceRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _repository.CreateServiceRequestAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateServiceRequestStatus request)
    {
        if (!Enum.IsDefined(typeof(RequestStatus), request.Status))
            return BadRequest(new { message = "Invalid status value" });

        var success = await _repository.UpdateServiceRequestStatusAsync(id, request.Status);
        if (!success)
            return NotFound(new { message = $"Service request with ID {id} not found" });

        return NoContent();
    }
}