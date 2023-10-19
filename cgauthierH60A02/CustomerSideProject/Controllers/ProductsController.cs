using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;

namespace CustomerSideProject.Controllers
{
    public class ProductsController : Controller
    {
        public IStoreRepository<Product> _storeRepository;
        public IStoreRepository<ProductCategory> _categoryRepository;
        public ProductsController(IStoreRepository<Product> storeRepository, IStoreRepository<ProductCategory> categoryRepository)
        {
            _storeRepository = storeRepository;
            _categoryRepository = categoryRepository;
            ViewBag.categories = categoryRepository.GetAllAsync().Result;
        }


        public async Task<IActionResult> Index(string? name)
        {
            try
            {
                var products = string.IsNullOrEmpty(name) ? (await _storeRepository.GetAllAsync()).OrderBy(x => x.Description).ToList() : (await _storeRepository.GetAllAsync()).Where(x => x.Description.ToUpper().StartsWith(name.ToUpper())).OrderBy(x => x.Description).ToList();
                ViewBag.Img = await _storeRepository.GetImages(products);
                ViewBag.AllCategories = (await _categoryRepository.GetAllAsync()).ToDictionary(x => x.CategoryId);
                ViewBag.Title = string.IsNullOrEmpty(name) ? "All Products" : $"All Products with name \"{name}\"";
                return View(products);
            }
            catch {
                return View(new List<Product>()) ;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsForCategoryAsync(int id) {
            try
            {
                ViewBag.Title = (await _categoryRepository.Get(id)).ProdCat;
                ViewBag.Img = await _storeRepository.GetImages(await _storeRepository.GetAllAsync());
                var itemsCategory = (await _storeRepository.GetAllAsync()).Where(x => x.ProdCatId == id).OrderBy(x => x.Description);
                ViewBag.AllCategories = (await _categoryRepository.GetAllAsync()).Where(x => x.CategoryId == id).ToDictionary(x => x.CategoryId);
                return View("Index", itemsCategory);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }

        public async Task<IActionResult> GetProductsCategory(int? id)
        {
            try { 
            ViewBag.Img = await _storeRepository.GetImages(await _storeRepository.GetAllAsync());
            var itemsCategory = (await _storeRepository.GetAllAsync()).Where(x => x.ProdCatId == id).OrderBy(x => x.Description);
            try
            {
                ViewBag.AllCategories = (await _categoryRepository.GetAllAsync()).Where(x => x.CategoryId == id).ToDictionary(x => x.CategoryId);
            }
            catch
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            return View("Index", itemsCategory);
        }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
    }
}
        
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null || !(await _storeRepository.GetAllAsync()).Any())
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }
                //function call
                var product = await _storeRepository.Get(id);
                if (product == null)
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }
                if (product.Image != null)
                {
                    ViewData["img"] = await _storeRepository.RetrieveImage(product);
                }
                ViewData["Category"] = (await _categoryRepository.Get(product.ProdCatId)).ProdCat;
                return View(product);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }
        public async Task<IActionResult> GetOrderedByCategoryItems()
        {
            ViewBag.Title = "All products sorted by category name";
            ViewBag.Img = await _storeRepository.GetImages(await _storeRepository.GetAllAsync());
            var itemsOrdered = (await _storeRepository.GetAllAsync()).OrderBy(x => x.ProdCat.ProdCat).ThenBy(x => x.Description).ToList();
            ViewBag.AllCategories = (await _categoryRepository.GetAllAsync()).ToDictionary(x => x.CategoryId);
            return View("Index", itemsOrdered);
        }




    }
}
