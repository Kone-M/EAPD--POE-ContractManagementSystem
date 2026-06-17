using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContractManagement.API.Models;
using ContractManagement.API.Repositories;

namespace ContractManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContractsController : ControllerBase
{
    private readonly IContractRepository _repository;

    public ContractsController(IContractRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Contract>), 200)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? status,
        [FromQuery] string? serviceLevel,
        [FromQuery] string? search)
    {
        var contracts = await _repository.GetAllContractsAsync(status, serviceLevel, search);
        return Ok(contracts);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Contract), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var contract = await _repository.GetContractByIdAsync(id);
        if (contract == null)
            return NotFound(new { message = $"Contract with ID {id} not found" });
        return Ok(contract);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Contract), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Contract contract)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (contract.StartDate >= contract.EndDate)
            return BadRequest(new { message = "End date must be after start date" });

        var created = await _repository.CreateContractAsync(contract);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
    {
        if (!Enum.IsDefined(typeof(ContractStatus), request.Status))
            return BadRequest(new { message = "Invalid status value" });

        var success = await _repository.UpdateContractStatusAsync(id, request.Status);
        if (!success)
            return NotFound(new { message = $"Contract with ID {id} not found" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteContractAsync(id);
        if (!success)
            return BadRequest(new { message = "Cannot delete contract with existing service requests or contract not found" });
        return NoContent();
    }

    [HttpGet("client/{clientId}")]
    [ProducesResponseType(typeof(IEnumerable<Contract>), 200)]
    public async Task<IActionResult> GetByClient(int clientId)
    {
        var contracts = await _repository.GetContractsByClientIdAsync(clientId);
        return Ok(contracts);
    }

    [HttpGet("statistics")]
    [ProducesResponseType(typeof(object), 200)]
    public async Task<IActionResult> GetStatistics()
    {
        var total = await _repository.GetTotalContractsCountAsync();
        var stats = await _repository.GetContractStatisticsAsync();
        var revenue = await _repository.GetTotalRevenueAsync();

        return Ok(new
        {
            TotalContracts = total,
            Statistics = stats,
            TotalRevenue = revenue,
            ActiveContracts = stats.GetValueOrDefault(ContractStatus.Active, 0)
        });
    }
}