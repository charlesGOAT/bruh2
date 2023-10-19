using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;

namespace ModelsLibrary
{
    public partial class Product
    {
        public int ProductId { get; set; }
        [Display(Name = "Category")]
        public int ProdCatId { get; set; }
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters")]
        [SameDescriptionValidation("ProductId")]
        [Required]

       
        public string? Description { get; set; }
        [DataType(DataType.Text)]
        [StringLength(50,ErrorMessage = "Manufacturer cannot be longer than 50 characters")]
        [Required]
        public string? Manufacturer { get; set; }
        [Required]
       
        
        public int Stock { get; set; }
        [DataType(DataType.Currency)]
        [RegularExpression("^[0-9]*\\.?[0-9]{1,2}$", ErrorMessage = "There can only be 1 or 2 numbers after the decimal point. It must be positive and a number")]
        [Required]
        [Display(Name = "Buy Price")]
       
        public decimal? BuyPrice { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        [Display(Name = "Selling Price")]
        [RegularExpression("^[0-9]*\\.?[0-9]{1,2}$", ErrorMessage = "There can only be 1 or 2 numbers after the decimal point. It must be positive and a number")]
        [SellPriceValidation("BuyPrice")]
       
        public decimal? SellPrice { get; set; }
        public byte[]? Image { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<CartItem>? CartItems { get; set; }

        public virtual ICollection<OrderItem>? OrderItems { get; set; }

   
       

        public virtual ProductCategory ProdCat { get; set; } = null!;

        public Product(int productId, int prodCatId, string? description, string? manufacturer, int stock, decimal? buyPrice, decimal? sellPrice)
        {
            ProductId = productId;
            ProdCatId = prodCatId;
            Description = description;
            Manufacturer = manufacturer;
            Stock = stock;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
        }

        public Product(int productId, int prodCatId, string? description, string? manufacturer, int stock, decimal? buyPrice, decimal? sellPrice, ProductCategory prodCat) : this(productId, prodCatId, description, manufacturer, stock, buyPrice, sellPrice)
        {
            ProdCat = prodCat;
        }

        public Product()
        {
        }
    }
}
