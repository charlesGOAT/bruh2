using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Net.Http.Headers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.Serialization.Json;
using ModelsLibrary;

namespace H60AssignmentDB_cgauthier.Controllers
{
    [Authorize(Roles = "Clerk,Manager")]
    public class ProductCategoriesController : Controller
    {
        private IStoreRepository<ProductCategory> _storeRepository;
        public ProductCategoriesController(IStoreRepository<ProductCategory> storeRepository)
        {
            this._storeRepository = storeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            var categories = (await _storeRepository.GetAllAsync()).OrderBy(x => x.ProdCat);
            ViewBag.Img = await _storeRepository.GetImages(categories.ToList());
            return View(categories);
        }
        
        
       
        // GET: ProductCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,ProdCat")] ProductCategory productCategory)
        {

            HttpClient Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            if (ModelState.IsValid)
            {
                await _storeRepository.Create(productCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || !(await _storeRepository.GetAllAsync()).Any() || id == 1)
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }

                var productCategory = await _storeRepository.Get(id);
                if (productCategory == null)
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }
                return View(productCategory);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error",controllerName:"Home");
            }
            }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,ProdCat")] ProductCategory productCategory)
        {

            if (id != productCategory.CategoryId || id == 1)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var myProduct = await _storeRepository.Get(id);
                    if (myProduct == null)
                    {
                        return RedirectToAction("Error", controllerName: "Home");
                    }
                    myProduct.ProdCat = productCategory.ProdCat;
                    
                   
                    await _storeRepository.Update(myProduct);
                }
                catch
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || !(await _storeRepository.GetAllAsync()).Any() || id == 1)
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }

                var productCategory = await _storeRepository.Get(id);
                if (productCategory == null)
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }

                return View(productCategory);
            }
            catch (HttpRequestException e) {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }

        public async Task<IActionResult> UploadImage(int? id)
        {
            try
            {
                ProductCategory category = await _storeRepository.Get(id);
            if (category == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            if (category.Image != null)
            {
                ViewBag.PreviousImage = await _storeRepository.RetrieveImage(category);
            }
            if (category == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            return View(category);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(ProductCategory category)
        {
            try
            {
                ProductCategory Mycategory = await _storeRepository.Get(category.CategoryId);
            if (Mycategory == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            if (Mycategory.Image != null)
            {
                ViewBag.PreviousImage = await _storeRepository.RetrieveImage(Mycategory);
            }
            if (!Request.Form.Files.Any())
            {
                ViewData["Message"] = "You must choose an image";
                return View(Mycategory);
            }
            var file = Request.Form.Files[0];



            if (!(await _storeRepository.ValidateFile(file)))
            {
                ViewData["Message"] = "The image must be of type png, jpeg, jpg or gif. It's length must be smaller than 2mb";
                return View(Mycategory);
            }

            await _storeRepository.UploadImage(Mycategory, file);

            return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }
        public async Task<IActionResult> DeleteImage(int? id)
        {
            try
            {
                var MyCategory = await _storeRepository.Get(id);

            if (MyCategory == null)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
            if (MyCategory.Image != null && MyCategory != null)
            {
                MyCategory.Image = null;
                await _storeRepository.Update(MyCategory);
            }
            return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }


        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var MyCategory = await _storeRepository.Get(id);

                if (id == 1)
                {
                    return RedirectToAction("Error", controllerName: "Home");
                }
                if (!(await _storeRepository.GetAllAsync()).Any())
                {
                    return Problem("Entity set 'H60AssignmentDB_cgauthierContext.ProductCategories'  is null.");
                }
                var productCategory = await _storeRepository.Get(id);
                if (productCategory != null)
                {
                    await _storeRepository.Delete(productCategory.CategoryId);
                }


                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("Error", controllerName: "Home");
            }
        }
        
        
    }
}
