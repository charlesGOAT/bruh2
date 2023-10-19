using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace ModelsLibrary
{
    public class SameCategoryValidation:ValidationAttribute
    {

        public string CategoryId{ get; private set; }
        public SameCategoryValidation(string _CategoryId) : base()
        {
            CategoryId = _CategoryId;
        }
        public async Task<List<ProductCategory>> GetProductsAsync() {
            var client = new HttpClient();
            var productCategories = await client.GetFromJsonAsync<List<ProductCategory>>("http://localhost:47733/api/ProductsCategoryApi");
            return productCategories;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           
            var Id = validationContext.ObjectInstance.GetType().GetProperty(CategoryId);
            var MyCategoryId = Id.GetValue(validationContext.ObjectInstance, null);
            if (value == null)
            {
                return new ValidationResult("Please enter a value");
            }

            if (GetProductsAsync().Result.Any(x => x.ProdCat.ToLower() == value.ToString().ToLower()) && value.ToString().ToLower() != GetProductsAsync().Result.Where(x => x.CategoryId == Convert.ToInt32(MyCategoryId)).FirstOrDefault()?.ProdCat.ToLower())
            {
                return new ValidationResult("A record with the same name already exists");
            }
            return ValidationResult.Success;
        }


    }
}
