using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContractManagement.API.Models;
using ContractManagement.API.Repositories;

namespace ContractManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientsController : ControllerBase
{
    private readonly IContractRepository _repository;

    public ClientsController(IContractRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _repository.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Client), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var client = await _repository.GetClientByIdAsync(id);
        if (client == null)
            return NotFound(new { message = $"Client with ID {id} not found" });
        return Ok(client);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Client), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Client client)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _repository.CreateClientAsync(client);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] Client client)
    {
        if (id != client.Id)
            return BadRequest(new { message = "ID mismatch" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _repository.UpdateClientAsync(client);
        if (!success)
            return NotFound(new { message = $"Client with ID {id} not found" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteClientAsync(id);
        if (!success)
            return BadRequest(new { message = "Cannot delete client with existing contracts or client not found" });
        return NoContent();
    }
}