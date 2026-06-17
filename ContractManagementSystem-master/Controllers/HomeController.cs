using Microsoft.AspNetCore.Mvc;
using ContractManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ContractManagementSystem.Models;

namespace ContractManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.UtcNow;

            ViewBag.TotalContracts = await _context.Contracts.CountAsync();
            ViewBag.ActiveContracts = await _context.Contracts
                .CountAsync(c => c.Status == ContractStatus.Active &&
                                c.StartDate <= today &&
                                c.EndDate >= today);
            ViewBag.TotalClients = await _context.Clients.CountAsync();
            ViewBag.TotalServiceRequests = await _context.ServiceRequests.CountAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}