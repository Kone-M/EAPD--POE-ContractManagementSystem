using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ContractManagementSystem.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IContractService _contractService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContractsController(
            ApplicationDbContext context,
            IContractService contractService,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _contractService = contractService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Contracts (WITH SEARCH - Updated)
        public async Task<IActionResult> Index(string searchTerm, string status, string serviceLevel, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.ServiceRequests)
                .AsQueryable();

            // Search by Name, Email, Region
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(c =>
                    c.Client.Name.ToLower().Contains(searchTerm) ||
                    c.Client.Email.ToLower().Contains(searchTerm) ||
                    c.Client.Region.ToLower().Contains(searchTerm) ||
                    c.Status.ToString().ToLower().Contains(searchTerm) ||
                    c.ServiceLevel.ToString().ToLower().Contains(searchTerm)
                );
            }

            // Filter by Status
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<ContractStatus>(status, true, out var statusEnum))
            {
                query = query.Where(c => c.Status == statusEnum);
            }

            // Filter by Service Level
            if (!string.IsNullOrEmpty(serviceLevel) && Enum.TryParse<ServiceLevel>(serviceLevel, true, out var levelEnum))
            {
                query = query.Where(c => c.ServiceLevel == levelEnum);
            }

            // Filter by Date Range
            if (startDate.HasValue)
            {
                query = query.Where(c => c.StartDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(c => c.EndDate <= endDate.Value);
            }

            var contracts = await query
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            // Preserve search values for view
            ViewBag.SearchTerm = searchTerm;
            ViewBag.StatusFilter = status;
            ViewBag.ServiceLevelFilter = serviceLevel;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(contracts);
        }

        // GET: Contracts/AdvancedSearch
        public async Task<IActionResult> AdvancedSearch()
        {
            ViewBag.Clients = await _context.Clients
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToListAsync();
            ViewBag.StatusList = Enum.GetValues(typeof(ContractStatus)).Cast<ContractStatus>().Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() });
            ViewBag.LevelList = Enum.GetValues(typeof(ServiceLevel)).Cast<ServiceLevel>().Select(l => new SelectListItem { Value = l.ToString(), Text = l.ToString() });
            return View();
        }

        // POST: Contracts/AdvancedSearch
        [HttpPost]
        public async Task<IActionResult> AdvancedSearch(int? clientId, DateTime? startDateFrom, DateTime? startDateTo,
            DateTime? endDateFrom, DateTime? endDateTo, string status, string serviceLevel, decimal? minCost, decimal? maxCost)
        {
            var query = _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.ServiceRequests)
                .AsQueryable();

            if (clientId.HasValue && clientId.Value > 0)
                query = query.Where(c => c.ClientId == clientId.Value);

            if (startDateFrom.HasValue)
                query = query.Where(c => c.StartDate >= startDateFrom.Value);
            if (startDateTo.HasValue)
                query = query.Where(c => c.StartDate <= startDateTo.Value);

            if (endDateFrom.HasValue)
                query = query.Where(c => c.EndDate >= endDateFrom.Value);
            if (endDateTo.HasValue)
                query = query.Where(c => c.EndDate <= endDateTo.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(c => c.Status.ToString() == status);

            if (!string.IsNullOrEmpty(serviceLevel))
                query = query.Where(c => c.ServiceLevel.ToString() == serviceLevel);

            if (minCost.HasValue)
                query = query.Where(c => c.ServiceRequests.Sum(sr => sr.Cost) >= minCost.Value);
            if (maxCost.HasValue)
                query = query.Where(c => c.ServiceRequests.Sum(sr => sr.Cost) <= maxCost.Value);

            var contracts = await query.OrderByDescending(c => c.CreatedAt).ToListAsync();

            ViewBag.SearchResults = contracts;
            ViewBag.ClientId = clientId;
            ViewBag.StartDateFrom = startDateFrom?.ToString("yyyy-MM-dd");
            ViewBag.StartDateTo = startDateTo?.ToString("yyyy-MM-dd");
            ViewBag.EndDateFrom = endDateFrom?.ToString("yyyy-MM-dd");
            ViewBag.EndDateTo = endDateTo?.ToString("yyyy-MM-dd");
            ViewBag.Status = status;
            ViewBag.ServiceLevel = serviceLevel;
            ViewBag.MinCost = minCost;
            ViewBag.MaxCost = maxCost;

            ViewBag.Clients = await _context.Clients.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToListAsync();
            ViewBag.StatusList = Enum.GetValues(typeof(ContractStatus)).Cast<ContractStatus>().Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() });
            ViewBag.LevelList = Enum.GetValues(typeof(ServiceLevel)).Cast<ServiceLevel>().Select(l => new SelectListItem { Value = l.ToString(), Text = l.ToString() });

            return View();
        }

        // GET: Contracts/ViewDatabase
        public async Task<IActionResult> ViewDatabase()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.ServiceRequests)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            ViewBag.TotalContracts = contracts.Count;
            ViewBag.TotalActive = contracts.Count(c => c.IsActive);
            ViewBag.TotalExpired = contracts.Count(c => c.Status == ContractStatus.Expired);
            ViewBag.TotalDraft = contracts.Count(c => c.Status == ContractStatus.Draft);
            ViewBag.TotalOnHold = contracts.Count(c => c.Status == ContractStatus.OnHold);
            ViewBag.TotalServiceRequests = contracts.Sum(c => c.ServiceRequests?.Count ?? 0);
            ViewBag.TotalRevenue = contracts.Sum(c => c.TotalServiceCost);

            return View(contracts);
        }

        // GET: Contracts/ExportToExcel
        public async Task<IActionResult> ExportToExcel()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.ServiceRequests)
                .ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Contract ID,Client Name,Email,Region,Start Date,End Date,Status,Service Level,Service Requests,Total Cost (USD),Total Cost (ZAR)");

            foreach (var contract in contracts)
            {
                csv.AppendLine($"{contract.Id},{contract.Client.Name},{contract.Client.Email},{contract.Client.Region}," +
                    $"{contract.StartDate:yyyy-MM-dd},{contract.EndDate:yyyy-MM-dd},{contract.Status},{contract.ServiceLevel}," +
                    $"{contract.ServiceRequestCount},{contract.TotalServiceCost:C},{(contract.TotalServiceCost * 19.5m):C}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", $"Contracts_{DateTime.Now:yyyyMMdd}.csv");
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var contract = await _context.Contracts
                .Include(c => c.Client)
                .Include(c => c.ServiceRequests)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (contract == null) return NotFound();
            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewBag.Clients = _context.Clients
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = $"{c.Name} - {c.Region}" })
                .ToList();
            return View();
        }

        // POST: Contracts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,StartDate,EndDate,Status,ServiceLevel")] Contract contract, IFormFile signedAgreement)
        {
            ModelState.Remove("SignedAgreementPath");
            ModelState.Remove("SignedAgreementFileName");

            if (ModelState.IsValid)
            {
                if (contract.StartDate >= contract.EndDate)
                {
                    ModelState.AddModelError("EndDate", "End date must be after start date");
                    ViewBag.Clients = _context.Clients.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = $"{c.Name} - {c.Region}" }).ToList();
                    return View(contract);
                }

                if (signedAgreement != null && signedAgreement.Length > 0)
                {
                    var isValid = await _contractService.ValidateFileUpload(signedAgreement.OpenReadStream(), signedAgreement.FileName);
                    if (!isValid)
                    {
                        ModelState.AddModelError("signedAgreement", "Only PDF files are allowed");
                        ViewBag.Clients = _context.Clients.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = $"{c.Name} - {c.Region}" }).ToList();
                        return View(contract);
                    }

                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "contracts");
                    Directory.CreateDirectory(uploadsFolder);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + signedAgreement.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                        await signedAgreement.CopyToAsync(fileStream);
                    contract.SignedAgreementPath = Path.Combine("uploads", "contracts", uniqueFileName);
                    contract.SignedAgreementFileName = signedAgreement.FileName;
                }

                contract.CreatedAt = DateTime.UtcNow;
                _context.Add(contract);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Contract created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clients = _context.Clients.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = $"{c.Name} - {c.Region}" }).ToList();
            return View(contract);
        }

        // GET: Contracts/DownloadAgreement/5
        public async Task<IActionResult> DownloadAgreement(int id)
        {
            try
            {
                var contract = await _context.Contracts.FindAsync(id);
                if (contract == null || string.IsNullOrEmpty(contract.SignedAgreementPath))
                {
                    TempData["Error"] = "No agreement file found.";
                    return RedirectToAction(nameof(Details), new { id = id });
                }

                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, contract.SignedAgreementPath);
                if (!System.IO.File.Exists(filePath))
                {
                    TempData["Error"] = "File not found.";
                    return RedirectToAction(nameof(Details), new { id = id });
                }

                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileBytes, "application/pdf", contract.SignedAgreementFileName);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction(nameof(Details), new { id = id });
            }
        }

        // GET: Contracts/CreateServiceRequest/5
        public async Task<IActionResult> CreateServiceRequest(int contractId)
        {
            var contract = await _context.Contracts.Include(c => c.Client).FirstOrDefaultAsync(c => c.Id == contractId);
            if (contract == null) return NotFound();

            var canCreate = await _contractService.CanCreateServiceRequestAsync(contractId);
            if (!canCreate)
            {
                TempData["Error"] = "Cannot create service request for expired or on-hold contracts.";
                return RedirectToAction(nameof(Details), new { id = contractId });
            }

            var serviceRequest = new ServiceRequest { ContractId = contractId };
            ViewBag.ContractInfo = contract;
            return View(serviceRequest);
        }

        // POST: Contracts/CreateServiceRequest
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateServiceRequest([Bind("ContractId,Description,Cost,Status")] ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                var canCreate = await _contractService.CanCreateServiceRequestAsync(serviceRequest.ContractId);
                if (!canCreate)
                {
                    TempData["Error"] = "Cannot create service request for expired or on-hold contracts.";
                    return RedirectToAction(nameof(Details), new { id = serviceRequest.ContractId });
                }

                serviceRequest.CreatedAt = DateTime.UtcNow;
                if (serviceRequest.Status == RequestStatus.Completed)
                    serviceRequest.CompletedAt = DateTime.UtcNow;

                _context.Add(serviceRequest);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Service request created successfully!";
                return RedirectToAction(nameof(Details), new { id = serviceRequest.ContractId });
            }

            var contract = await _context.Contracts.FindAsync(serviceRequest.ContractId);
            ViewBag.ContractInfo = contract;
            return View(serviceRequest);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) return NotFound();
            ViewBag.Clients = _context.Clients.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = $"{c.Name} - {c.Region}", Selected = c.Id == contract.ClientId }).ToList();
            return View(contract);
        }

        // POST: Contracts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,StartDate,EndDate,Status,ServiceLevel")] Contract contract)
        {
            if (id != contract.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.Contracts.FindAsync(id);
                    if (existing == null) return NotFound();
                    existing.ClientId = contract.ClientId;
                    existing.StartDate = contract.StartDate;
                    existing.EndDate = contract.EndDate;
                    existing.Status = contract.Status;
                    existing.ServiceLevel = contract.ServiceLevel;
                    _context.Update(existing);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Contract updated successfully!";
                }
                catch (DbUpdateConcurrencyException) { if (!ContractExists(contract.Id)) return NotFound(); else throw; }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clients = _context.Clients.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = $"{c.Name} - {c.Region}", Selected = c.Id == contract.ClientId }).ToList();
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _context.Contracts.Include(c => c.Client).Include(c => c.ServiceRequests).FirstOrDefaultAsync(c => c.Id == id);
            if (contract == null) return NotFound();
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.Include(c => c.ServiceRequests).FirstOrDefaultAsync(c => c.Id == id);
            if (contract != null)
            {
                if (contract.ServiceRequests != null && contract.ServiceRequests.Any())
                {
                    TempData["Error"] = "Cannot delete contract with existing service requests.";
                    return RedirectToAction(nameof(Index));
                }

                if (!string.IsNullOrEmpty(contract.SignedAgreementPath))
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, contract.SignedAgreementPath);
                    if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
                }

                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Contract deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id) => _context.Contracts.Any(e => e.Id == id);
    }
}