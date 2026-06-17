using ContractManagement.API.Models;

namespace ContractManagement.API.Repositories;

public interface IContractRepository
{
    // Contract operations
    Task<IEnumerable<Contract>> GetAllContractsAsync(string? status = null, string? serviceLevel = null, string? search = null);
    Task<Contract?> GetContractByIdAsync(int id);
    Task<Contract> CreateContractAsync(Contract contract);
    Task<bool> UpdateContractStatusAsync(int id, ContractStatus status);
    Task<bool> DeleteContractAsync(int id);
    Task<IEnumerable<Contract>> GetContractsByClientIdAsync(int clientId);

    // Service Request operations
    Task<IEnumerable<ServiceRequest>> GetServiceRequestsByContractIdAsync(int contractId);
    Task<ServiceRequest?> GetServiceRequestByIdAsync(int id);
    Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequest request);
    Task<bool> UpdateServiceRequestStatusAsync(int id, RequestStatus status);

    // Client operations
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client?> GetClientByIdAsync(int id);
    Task<Client> CreateClientAsync(Client client);
    Task<bool> UpdateClientAsync(Client client);
    Task<bool> DeleteClientAsync(int id);

    // Statistics
    Task<int> GetTotalContractsCountAsync();
    Task<Dictionary<ContractStatus, int>> GetContractStatisticsAsync();
    Task<decimal> GetTotalRevenueAsync();
}