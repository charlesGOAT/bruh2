using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace ModelsLibrary
{
    public class SellPriceValidation :ValidationAttribute
    {
        public string BuyPrice { get; private set; }
        public SellPriceValidation(string _BuyPrice):base() { 
        BuyPrice = _BuyPrice;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var Buy = validationContext.ObjectInstance.GetType().GetProperty(BuyPrice);
            var  BuyValue = Buy.GetValue(validationContext.ObjectInstance, null);
            try
            {
                if (Convert.ToDouble(value) < Convert.ToDouble(BuyValue))
                {
                    return new ValidationResult("The Sell price must be greater than the Buy price");
                }
                else if (Convert.ToDouble(value) >= 1000000)
                {
                    return new ValidationResult("The price is too big for the system. Please enter a lesser value");
                }
            }
            catch (Exception ex) {
                return new ValidationResult("A problem as occured. Please enter another value");
            }
            return ValidationResult.Success;
        }

    }
}
