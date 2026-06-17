using Microsoft.EntityFrameworkCore;
using ContractManagement.API.Data;
using ContractManagement.API.Models;

namespace ContractManagement.API.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _context;

    public ContractRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contract>> GetAllContractsAsync(string? status = null, string? serviceLevel = null, string? search = null)
    {
        var query = _context.Contracts
            .Include(c => c.Client)
            .Include(c => c.ServiceRequests)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<ContractStatus>(status, true, out var statusEnum))
            query = query.Where(c => c.Status == statusEnum);

        if (!string.IsNullOrEmpty(serviceLevel) && Enum.TryParse<ServiceLevel>(serviceLevel, true, out var levelEnum))
            query = query.Where(c => c.ServiceLevel == levelEnum);

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            query = query.Where(c =>
                (c.Client != null && c.Client.Name.ToLower().Contains(search)) ||
                (c.Client != null && c.Client.Email.ToLower().Contains(search)) ||
                (c.Client != null && c.Client.Region.ToLower().Contains(search)) ||
                c.Status.ToString().ToLower().Contains(search) ||
                c.ServiceLevel.ToString().ToLower().Contains(search));
        }

        return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public async Task<Contract?> GetContractByIdAsync(int id)
    {
        return await _context.Contracts
            .Include(c => c.Client)
            .Include(c => c.ServiceRequests)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contract> CreateContractAsync(Contract contract)
    {
        contract.CreatedAt = DateTime.UtcNow;
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();
        return contract;
    }

    public async Task<bool> UpdateContractStatusAsync(int id, ContractStatus status)
    {
        var contract = await _context.Contracts.FindAsync(id);
        if (contract == null) return false;

        contract.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteContractAsync(int id)
    {
        var contract = await _context.Contracts
            .Include(c => c.ServiceRequests)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contract == null || contract.ServiceRequests.Any()) return false;

        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Contract>> GetContractsByClientIdAsync(int clientId)
    {
        return await _context.Contracts
            .Include(c => c.Client)
            .Include(c => c.ServiceRequests)
            .Where(c => c.ClientId == clientId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceRequest>> GetServiceRequestsByContractIdAsync(int contractId)
    {
        return await _context.ServiceRequests
            .Where(sr => sr.ContractId == contractId)
            .OrderByDescending(sr => sr.CreatedAt)
            .ToListAsync();
    }

    public async Task<ServiceRequest?> GetServiceRequestByIdAsync(int id)
    {
        return await _context.ServiceRequests
            .Include(sr => sr.Contract)
            .ThenInclude(c => c!.Client)
            .FirstOrDefaultAsync(sr => sr.Id == id);
    }

    public async Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequest request)
    {
        request.CreatedAt = DateTime.UtcNow;
        _context.ServiceRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task<bool> UpdateServiceRequestStatusAsync(int id, RequestStatus status)
    {
        var request = await _context.ServiceRequests.FindAsync(id);
        if (request == null) return false;

        request.Status = status;
        if (status == RequestStatus.Completed)
            request.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _context.Clients
            .Include(c => c.Contracts)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _context.Clients
            .Include(c => c.Contracts)
            .ThenInclude(c => c!.ServiceRequests)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Client> CreateClientAsync(Client client)
    {
        client.CreatedAt = DateTime.UtcNow;
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<bool> UpdateClientAsync(Client client)
    {
        var existing = await _context.Clients.FindAsync(client.Id);
        if (existing == null) return false;

        existing.Name = client.Name;
        existing.Email = client.Email;
        existing.Phone = client.Phone;
        existing.Address = client.Address;
        existing.Region = client.Region;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteClientAsync(int id)
    {
        var client = await _context.Clients
            .Include(c => c.Contracts)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (client == null || client.Contracts.Any()) return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetTotalContractsCountAsync()
    {
        return await _context.Contracts.CountAsync();
    }

    public async Task<Dictionary<ContractStatus, int>> GetContractStatisticsAsync()
    {
        return await _context.Contracts
            .GroupBy(c => c.Status)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        return await _context.ServiceRequests
            .Where(sr => sr.Status == RequestStatus.Completed)
            .SumAsync(sr => sr.Cost);
    }
}