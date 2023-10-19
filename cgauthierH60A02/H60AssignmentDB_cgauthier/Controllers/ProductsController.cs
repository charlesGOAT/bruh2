using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using ModelsLibrary;

namespace H60AssignmentDB_cgauthier.Controllers
{
    [Authorize(Roles ="Clerk,Manager")]
    public class ProductsController : Controller
    {
      
        private IStoreRepository<Product> _storeRepository;
        private IStoreRepository<ProductCategory> _prodCatRepository;
            public ProductsController(IStoreRepository<Product> storeRepository, IStoreRepository<ProductCategory> prodCatRepository )
        {
            _storeRepository = storeRepository;
            _prodCatRepository = prodCatRepository;
        }


        // GET: Products
        public async Task<IActionResult> Index(string? name)
        {
            var products = string.IsNullOrEmpty(name) ? (await _storeRepository.GetAllAsync()).OrderBy(x => x.Description).ToList() : (await _storeRepository.GetAllAsync()).Where(x => x.Description.ToUpper().StartsWith(name.ToUpper())).OrderBy(x => x.Description).ToList();
            ViewBag.Img = await _storeRepository.GetImages(products);
            ViewBag.Title = string.IsNullOrEmpty(name) ? "All Products" : $"All Products with name \"{name}\"";
            return View(products);
        }

        // GET: Products/Details/5
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
                ViewData["Category"] = (await _prodCatRepository.Get(product.ProdCatId)).ProdCat;
                return View(product);
            }catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProdCatId"] = new SelectList((await _prodCatRepository.GetAllAsync()).Where(x => x.CategoryId != 1), "CategoryId", "ProdCat");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {


            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState["ProdCat"].ValidationState = ModelValidationState.Valid;
                //ModelState["CartItems"].ValidationState = ModelValidationState.Valid;
                //ModelState["OrderItems"].ValidationState = ModelValidationState.Valid;
            }
            if (ModelState.IsValid && product.Stock >=0 )
            {
              await _storeRepository.Create(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdCatId"] = new SelectList((await _prodCatRepository.GetAllAsync()).Where(x => x.CategoryId != 1), "CategoryId", "ProdCat", product.ProdCatId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var product = await _storeRepository.Get(id);
                if (id == null || !(await _storeRepository.GetAllAsync()).Any() || product == null)
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }

                ViewBag.ProdCatId = new SelectList((await _prodCatRepository.GetAllAsync()), "CategoryId", "ProdCat", product.ProdCatId);
                return View(product);
            }
            catch (HttpRequestException e) {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProdCatId,Description,Manufacturer,Stock,BuyPrice,SellPrice")] Product product)
        {
            try
            {
                var myProduct = await _storeRepository.Get(product.ProductId);
           
            if (myProduct == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }

            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState["ProdCat"].ValidationState = ModelValidationState.Valid;
                //ModelState["CartItems"].ValidationState = ModelValidationState.Valid;
                //ModelState["OrderItems"].ValidationState = ModelValidationState.Valid;
            }
            if (id != product.ProductId)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var image = (await _storeRepository.Get(product.ProductId)).Image;
                    product.Image = image;
                  await  _storeRepository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _storeRepository.GetAllAsync()).Any(x => x.ProductId == product.ProductId))
                    {
                        return RedirectToAction("Error", controllerName: "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ProdCatId = new SelectList((await _prodCatRepository.GetAllAsync()), "CategoryId", "ProdCat", product.ProdCatId);
            return View(product);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var product = await _storeRepository.Get(id);
                if (id == null || !(await _storeRepository.GetAllAsync()).Any() || product == null)
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }


                ViewData["Category"] = (await _prodCatRepository.Get(product.ProdCatId)).ProdCat;
                return View(product);
            }catch(HttpRequestException e) {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (await _storeRepository.GetAllAsync() == null)
            {
                return Problem("Entity set 'H60AssignmentDB_cgauthierContext.Products'  is null.");
            }
            var product = await _storeRepository.Get(id);
            if (product != null)
            {
                await _storeRepository.Delete(product.ProductId);
            }
            
           
            return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }

        //Will have to check for this


        [HttpGet]
        public async Task<IActionResult> UpdateStocks(int id) {
            try
            {
                var myProduct = await _storeRepository.Get(id);
            if (myProduct == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            return View( myProduct);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> UpdateSellAndBuyPrices(int id)
        {
            try
            {
                var myProduct = await _storeRepository.Get(id);
            if (myProduct == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            return View( myProduct);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStocks(int id, int stock) {
            try
            {
                var myProduct = await _storeRepository.Get(id);
            if (myProduct == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            if (ModelState.IsValid && myProduct.Stock + stock >= 0)
            {
                myProduct.Stock += stock;
               await _storeRepository.Update(myProduct);
                return RedirectToAction(nameof(Index));
            } else if (myProduct.Stock + stock < 0) {
                TempData["Error"] = "Current stock must not be less than 0";
            } 
            return View(myProduct);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSellAndBuyPrices(Product product) {
            try
            {
                var myProduct = await _storeRepository.Get(product.ProductId);
            if (myProduct == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState["ProdCat"].ValidationState = ModelValidationState.Valid;
                //ModelState["CartItems"].ValidationState = ModelValidationState.Valid;
                //ModelState["OrderItems"].ValidationState = ModelValidationState.Valid;
            }
            if (ModelState.IsValid) {
            
                myProduct.SellPrice = product.SellPrice;
                myProduct.BuyPrice = product.BuyPrice;
              await  _storeRepository.Update(myProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }
        public async Task<IActionResult> UploadImage(int? id) {
            try
            {
                Product product = await _storeRepository.Get(id);
            if (product == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            if (product.Image != null)
            {
                ViewBag.PreviousImage = await _storeRepository.RetrieveImage(product);
            }
            if(product==null){ 
            return RedirectToAction("Error", controllerName: "Home");
            }
            return View(product);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(Product product) {
            try
            {
                Product Myproduct = await _storeRepository.Get(product.ProductId);
            if (Myproduct == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            if (Myproduct.Image != null)
            {
                ViewBag.PreviousImage = await _storeRepository.RetrieveImage(Myproduct);
            }
            if (!Request.Form.Files.Any()) {
                ViewData["Message"] = "You must choose an image";
                return View(Myproduct);
            }
            var file = Request.Form.Files[0];
           
           

            if (!(await _storeRepository.ValidateFile(file))) {
                ViewData["Message"] = "The image must be of type png, jpeg, jpg or gif. It's length must be smaller than 2mb";
            return View(Myproduct);
            }

            await _storeRepository.UploadImage(product,file);
            
            return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }
        public async Task<IActionResult> DeleteImage(int? id) {
            try
            {
                var MyProduct = await _storeRepository.Get(id);
            
            if (MyProduct == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            if (MyProduct.Image != null && MyProduct != null)
            {
                MyProduct.Image = null;
               await _storeRepository.Update(MyProduct);
            }
                return RedirectToAction(nameof(Index));
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
            var itemsOrdered = (await _storeRepository.GetAllAsync()).OrderBy(x=>x.ProdCat.ProdCat).ThenBy(x=>x.Description).ToList();
            ViewBag.AllCategories =  (await _prodCatRepository.GetAllAsync()).ToDictionary(x=>x.CategoryId);
            return View("Index",itemsOrdered);
        }
        public async Task<IActionResult> GetProductsCategory(int? id)
        {
            ViewBag.Title = "Products by category name";
            ViewBag.Img = await _storeRepository.GetImages(await _storeRepository.GetAllAsync());
            var itemsCategory = (await _storeRepository.GetAllAsync()).Where(x=>x.ProdCatId == id).OrderBy(x => x.Description);
            try
            {
                ViewBag.AllCategories = (await _prodCatRepository.GetAllAsync()).Where(x => x.CategoryId == id).ToDictionary(x => x.CategoryId);
            }
            catch {
                return RedirectToAction("Error", controllerName: "Home");
            }
                return View("Index",itemsCategory);
        }
        public async Task<IActionResult> SortByStock() {
            ViewBag.Title = "Products sorted by stocks";
            ViewBag.Img = await _storeRepository.GetImages(await _storeRepository.GetAllAsync());
            var itemsOrdered = (await _storeRepository.GetAllAsync()).OrderByDescending(x=>x.Stock).ThenBy(x=>x.Description);
            return View("Index", itemsOrdered);
        }

        

    }
}
