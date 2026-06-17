using ContractManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractManagementSystem.Services
{
    public interface IContractService
    {
        Task<bool> CanCreateServiceRequestAsync(int contractId);
        Task<decimal> ConvertUsdToZar(decimal usdAmount);
        Task<bool> ValidateFileUpload(Stream fileStream, string fileName);
        Task<Contract?> GetContractWithDetailsAsync(int contractId);
        Task<decimal> GetTotalContractValue(int contractId);
        Task<Dictionary<ContractStatus, int>> GetContractStatistics();
    }

    public class ContractService : IContractService
    {
        private readonly Data.ApplicationDbContext _context;
        private const decimal UsdToZarRate = 19.5m;

        public ContractService(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CanCreateServiceRequestAsync(int contractId)
        {
            var contract = await _context.Contracts
                .FirstOrDefaultAsync(c => c.Id == contractId);

            if (contract == null)
                return false;

            // ServiceRequest cannot be created if parent Contract is "Expired" or "On Hold"
            return contract.Status != ContractStatus.Expired &&
                   contract.Status != ContractStatus.OnHold;
        }

        public Task<decimal> ConvertUsdToZar(decimal usdAmount)
        {
            // Verify that the math converting USD to ZAR is correct
            var zarAmount = usdAmount * UsdToZarRate;
            return Task.FromResult(zarAmount);
        }

        public async Task<bool> ValidateFileUpload(Stream fileStream, string fileName)
        {
            // Check file extension
            var extension = Path.GetExtension(fileName).ToLower();
            if (extension != ".pdf")
                return false;

            // Additional validation: check file header for PDF signature
            if (fileStream != null && fileStream.Length > 0)
            {
                fileStream.Position = 0;
                byte[] header = new byte[4];
                await fileStream.ReadAsync(header, 0, 4);
                fileStream.Position = 0;

                // PDF files start with %PDF
                string pdfHeader = System.Text.Encoding.ASCII.GetString(header);
                return pdfHeader.StartsWith("%PDF");
            }

            return true;
        }

        public async Task<Contract?> GetContractWithDetailsAsync(int contractId)
        {
            return await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.ServiceRequests)
                .FirstOrDefaultAsync(c => c.Id == contractId);
        }

        public async Task<decimal> GetTotalContractValue(int contractId)
        {
            var contract = await GetContractWithDetailsAsync(contractId);
            if (contract == null)
                return 0;

            return contract.ServiceRequests?.Sum(sr => sr.Cost) ?? 0;
        }

        public async Task<Dictionary<ContractStatus, int>> GetContractStatistics()
        {
            var stats = await _context.Contracts
                .GroupBy(c => c.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(k => k.Status, v => v.Count);

            return stats;
        }
    }
}