using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.Serialization.Json;

namespace ModelsLibrary
{
    public class ProductCategoriesRepository : IStoreRepository<ProductCategory>
    {
        

        public async Task Create(ProductCategory productCategory)
        {
            var Client = CreateClient();
            await Client.PostAsJsonAsync("http://localhost:47733/api/ProductsCategoryApi", productCategory);
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
                await Client.DeleteAsync($"http://localhost:47733/api/ProductsCategoryApi/{id}");
            
            
        }

        public async Task<ProductCategory> Get(int? id)
        {
            
            
                var Client = CreateClient();
                var productCategory = await Client.GetFromJsonAsync<ProductCategory>($"http://localhost:47733/api/ProductsCategoryApi/{id}");
                return productCategory;
            
            
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {

            var Client = CreateClient();
            var productCategories = await Client.GetFromJsonAsync<List<ProductCategory>>("http://localhost:47733/api/ProductsCategoryApi");
            return productCategories;

        }

        public async Task<Dictionary<int, string>> GetImages(List<ProductCategory> categories)
        {
            Dictionary<int, string> productsCategoryImgs = new();
            foreach (var x in categories)
            {
                if (x.Image != null)
                {
                    productsCategoryImgs.Add(x.CategoryId, await RetrieveImage(x));
                }

            }
            return productsCategoryImgs;
        }

        public async Task<string> RetrieveImage(ProductCategory category)
        {
            string imageBase64Data =
           Convert.ToBase64String(category.Image);
            string imageDataURL =
        string.Format("data:image/jpg;base64,{0}",
        imageBase64Data);
            return imageDataURL;
        }

        public async Task Update(ProductCategory productCategory)
        {
            var Client = CreateClient();
            await Client.PutAsJsonAsync($"http://localhost:47733/api/ProductsCategoryApi/{productCategory.CategoryId}", productCategory);
        }

        public async Task UploadImage(ProductCategory category, IFormFile file)
        {
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            var myCategory = await Get(category.CategoryId);
            myCategory.Image = ms.ToArray();
            ms.Close();
            ms.Dispose();
            await Update(myCategory);
        }

        public async Task<bool> ValidateFile(IFormFile file)
        {
            string[] ACCEPTABLE_FORMATS = { "image/jpeg", "image/png", "image/gif" };
            int TWOMB = 2000000;
            return (ACCEPTABLE_FORMATS.Contains(file.ContentType) && file.Length <= TWOMB);
        }
    }
}
