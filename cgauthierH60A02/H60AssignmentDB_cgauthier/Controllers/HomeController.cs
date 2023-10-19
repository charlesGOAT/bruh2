
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;
using System.Diagnostics;
using System.Security.Claims;

namespace H60AssignmentDB_cgauthier.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepository<Product> _storeRepository;

        public HomeController(ILogger<HomeController> logger, IStoreRepository<Product> storeRepository)
        {
            _storeRepository = storeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}