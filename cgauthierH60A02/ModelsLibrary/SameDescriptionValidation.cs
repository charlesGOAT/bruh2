using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace ModelsLibrary
{
    public class SameDescriptionValidation : ValidationAttribute
    {
        public string ProductId { get; private set; }
        public SameDescriptionValidation(string _ProductId) : base()
        {
            ProductId = _ProductId;
        }

        public async Task<List<Product>> GetProductsAsync() {
            var client = new HttpClient();
            return await client.GetFromJsonAsync<List<Product>>("http://localhost:47733/api/ProductsApi");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var Id = validationContext.ObjectInstance.GetType().GetProperty(ProductId);
            var MyProductId = Id.GetValue(validationContext.ObjectInstance, null);

            if (value == null)
            {
                return new ValidationResult("Please enter a value");
            }




            if (GetProductsAsync().Result.Any(x => x.Description == value.ToString()) && value.ToString().ToLower() != GetProductsAsync().Result.Where(x => x.ProductId == Convert.ToInt32(MyProductId)).FirstOrDefault()?.Description.ToLower())
            {
                return new ValidationResult("A record with the same name already exists");
            }
            return ValidationResult.Success;
        }

    }
}
