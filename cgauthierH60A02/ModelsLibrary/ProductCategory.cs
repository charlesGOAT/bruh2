using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }
        
        public int CategoryId { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Category")]
        [SameCategoryValidation("CategoryId")]
        [MaxLength(25)]
        [Required]
        
        public string ProdCat { get; set; } = null!;

        public byte[]? Image { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public ProductCategory(int categoryId, string prodCat)
        {
            CategoryId = categoryId;
            ProdCat = prodCat;
        }

        public ProductCategory(int categoryId, string prodCat, ICollection<Product> products) : this(categoryId, prodCat)
        {
            Products = products;
        }

    }
}
