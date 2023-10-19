using APIDBProject.Models;
using ModelsLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDBProject.Service
{

    public class ProductsCategoryService : ControllerBase, IStoreRepository<ProductCategory>
    {
        private readonly StoreContext _context;
        public ProductsCategoryService(StoreContext context)
        {
            _context = context;
        }

        //instead of new StoreContext maybe use using var cat ...
        public async Task Create(ProductCategory productCategory)
        {
            
            await _context.ProductCategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        //will have to remove from the repository
        public HttpClient CreateClient()
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            
            var products = _context.Products.Where(x => x.ProdCatId == id);
            foreach (var product in products)
            {
                product.ProdCatId = 1;
            }
            var productCategory = _context.ProductCategories.First(x=>x.CategoryId == id);
            //my need to change for what I had before where I queried the object from the context
            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        public async Task<ProductCategory> Get(int? id) {
          var myCategories =  await _context.ProductCategories.FirstOrDefaultAsync(x => x.CategoryId == id);
            return myCategories;
        }



        public async Task<List<ProductCategory>> GetAllAsync() => await _context.ProductCategories.ToListAsync();
      
        public Task<Dictionary<int, string>> GetImages(List<ProductCategory> categories)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveImage(ProductCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ProductCategory productCategory)
        {
           
            _context.ProductCategories.Update(productCategory);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        public Task UploadImage(ProductCategory productCategory, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateFile(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}