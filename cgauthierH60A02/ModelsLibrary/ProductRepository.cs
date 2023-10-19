using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.Serialization.Json;

namespace ModelsLibrary
{
    public class ProductRepository : IStoreRepository<Product>
    {
        public async Task Create(Product product)
        {
            var Client = CreateClient();
            var myProductCategory = await new ProductCategoriesRepository().Get(product.ProdCatId);
            product.ProdCat = myProductCategory;
            var res = await Client.PostAsJsonAsync("http://localhost:47733/api/ProductsApi", product);
            
            
        }
        public HttpClient CreateClient()
        {
            HttpClient Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            return Client;
        }

        public async Task Delete(int id)
        {
            var Client = CreateClient();
            await Client.DeleteAsync($"http://localhost:47733/api/ProductsApi/{id}");
        }

      
        public async Task<Product> Get(int? id)
        {
            var Client = CreateClient();

            var product = await Client.GetFromJsonAsync<Product>($"http://localhost:47733/api/ProductsApi/{id}");
            return product;
        }


        public async Task<List<Product>> GetAllAsync()
        {
            var Client = CreateClient();
            var products = await Client.GetFromJsonAsync<List<Product>>("http://localhost:47733/api/ProductsApi");
            products.OrderBy(x => x.Description);
            return products;
        }

        public async Task<Dictionary<int, string>> GetImages(List<Product> theProducts)
        {
            Dictionary<int, string> productsImgs = new();
            foreach (var x in theProducts)
            {
                if (x.Image != null)
                {
                    productsImgs.Add(x.ProductId, await RetrieveImage(x));
                }

            }
            return productsImgs;
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

        public async Task Update(Product product)
        {
            var Client = CreateClient();
            var myProductCategory = await new ProductCategoriesRepository().Get(product.ProdCatId);
            product.ProdCat = myProductCategory;
            await Client.PutAsJsonAsync($"http://localhost:47733/api/ProductsApi/{product.ProductId}",product);
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
            return (ACCEPTABLE_FORMATS.Contains(file.ContentType) && file.Length <= TWOMB);
 
        }
    }
}
