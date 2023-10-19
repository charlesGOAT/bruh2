using APIDBProject.Models;
using ModelsLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDBProject.Service
{
    public class ProductsService : ControllerBase, IStoreRepository<Product>
    {
        private readonly StoreContext _context;
        public ProductsService(StoreContext context)
        {
            _context = context;
        }

        public async Task Create(Product product)
        {
            
            product.ProdCat = null;
            product.Created = DateTime.Now;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

        }
        //Will have to remove this as the api does not take client

        public HttpClient CreateClient()
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            
            _context.Products.Remove(_context.Products.FirstOrDefault(x => x.ProductId == id));
            await _context.SaveChangesAsync();

        }

        public async Task<Product> Get(int? id) => await _context.Products.Include(x => x.ProdCat).FirstOrDefaultAsync(m => m.ProductId == id);


        public async Task<List<Product>> GetAllAsync() => await _context.Products.Include(x => x.ProdCat).ToListAsync();


        public async Task<Dictionary<int, string>> GetImages(List<Product> products)
        {
            Dictionary<int, string> productsImgs = new();
            var theProducts = (await GetAllAsync()).OrderBy(x=>x.Description);
            foreach (var x in theProducts)
            {
                if (x.Image != null)
                {
                    productsImgs.Add(x.ProductId, await RetrieveImage(x));
                }

            }
            return productsImgs;
        }
        //update will have to return something
        public async Task Update(Product product)
        {
            
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

        }


        public async Task<string> RetrieveImage(Product product)
        {
            
            string imageBase64Data =
            Convert.ToBase64String(product.Image);
            string imageDataURL =
        string.Format("data:image/jpg;base64,{0}",
        imageBase64Data);
            return imageDataURL;
        }

        public async Task UploadImage(Product product, IFormFile file)
        {
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            var MyProduct = await Get(product.ProductId);
            MyProduct.Image = ms.ToArray();
            ms.Close();
            ms.Dispose();
            await Update(MyProduct);
        }

        public async Task<bool> ValidateFile(IFormFile file)
        {
            string[] ACCEPTABLE_FORMATS = { "image/jpeg", "image/png", "image/gif" };
            int TWOMB = 2000000;
            if (ACCEPTABLE_FORMATS.Contains(file.ContentType) && file.Length <= TWOMB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

