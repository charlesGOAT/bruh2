using CustomerSideProject.Models;
using ModelsLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomerSideProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IStoreRepository<Product> _storeRepository;
        public IStoreRepository<ProductCategory> _categoryRepository;
        public HomeController(IStoreRepository<Product> storeRepository, IStoreRepository<ProductCategory> categoryRepository, ILogger<HomeController> logger)
        {
            _logger = logger;
            _storeRepository = storeRepository;
            _categoryRepository = categoryRepository;
            ViewBag.categories = categoryRepository.GetAllAsync().Result;
        }

        public async Task<IActionResult> Index()
        {
            var products = (await _storeRepository.GetAllAsync()).OrderBy(x => new Guid().ToString()).ToList();
            ViewBag.Img = await _storeRepository.GetImages(products);
            ViewBag.OrderedImages = await _storeRepository.GetImages(products.OrderBy(x => x.Created).ToList());
            return View(products);
        }

        
        
    }
}