using System;
using System.Collections.Generic;
using ModelsLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIDBProject.Models
{
    public partial class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:MyConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.ProdCatId, "IX_Product_ProdCatId");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.BuyPrice).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.SellPrice).HasColumnType("numeric(8, 2)");

                entity.HasOne(d => d.ProdCat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProdCatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductCategory");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("ProductCategory");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProdCat)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory() { CategoryId = 1, ProdCat = "Unknown" },
                new ProductCategory() { CategoryId = 2, ProdCat = "CPU" },
                new ProductCategory() { CategoryId = 3, ProdCat = "Motherboard" },
                new ProductCategory() { CategoryId = 4, ProdCat = "Peripherals" },
                new ProductCategory() { CategoryId = 5, ProdCat = "Ram" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 1, BuyPrice = 2000, SellPrice = 3000, ProdCatId = 2, Manufacturer = "Intel", Stock = 10, Description = "Intel 9900k I9",Created=DateTime.Now },
                new Product() { ProductId = 2, BuyPrice = 1000, SellPrice = 2000, ProdCatId = 3, Manufacturer = "Asus", Stock = 10, Description = "Asus ROG MAXIMUS", Created = DateTime.Now },
                new Product() { ProductId = 3, BuyPrice = 150, SellPrice = 350, ProdCatId = 2, Manufacturer = "AMD", Stock = 10, Description = "Ryzen 3", Created = DateTime.Now },
                new Product() { ProductId = 4, BuyPrice = 350, SellPrice = 570, ProdCatId = 2, Manufacturer = "Intel", Stock = 10, Description = "Intel Core i9-12900K", Created = DateTime.Now },
                new Product() { ProductId = 5, BuyPrice = 250, SellPrice = 350, ProdCatId = 2, Manufacturer = "AMD", Stock = 10, Description = "AMD Ryzen 7 5700X", Created = DateTime.Now },
                new Product() { ProductId = 6, BuyPrice = 150, SellPrice = 270, ProdCatId = 2, Manufacturer = "AMD", Stock = 10, Description = "AMD Ryzen 5 5500", Created = DateTime.Now },
                new Product() { ProductId = 7, BuyPrice = 250, SellPrice = 380, ProdCatId = 3, Manufacturer = "Asus", Stock = 10, Description = "Asus TUF GAMING X570-PLUS", Created = DateTime.Now },
                new Product() { ProductId = 8, BuyPrice = 260, SellPrice = 375, ProdCatId = 3, Manufacturer = "MSI", Stock = 10, Description = "MSI B550-A PRO", Created = DateTime.Now },
                new Product() { ProductId = 9, BuyPrice = 200, SellPrice = 330, ProdCatId = 3, Manufacturer = "Gigabyte", Stock = 10, Description = "Gigabyte B660M DS3H DDR4", Created = DateTime.Now },
                new Product() { ProductId = 10, BuyPrice = 190, SellPrice = 335, ProdCatId = 3, Manufacturer = "MSI", Stock = 10, Description = "MSI B450 TOMAHAWK MAX", Created = DateTime.Now },
                new Product() { ProductId = 11, BuyPrice = 100, SellPrice = 200, ProdCatId = 4, Manufacturer = "HP", Stock = 10, Description = "HP HyperX Cloud II", Created = DateTime.Now },
                new Product() { ProductId = 12, BuyPrice = 170, SellPrice = 300, ProdCatId = 4, Manufacturer = "Logitech", Stock = 10, Description = "Logitech Pro X 7.1", Created = DateTime.Now },
                new Product() { ProductId = 13, BuyPrice = 50, SellPrice = 95, ProdCatId = 4, Manufacturer = "Corsair", Stock = 10, Description = "Corsair K55", Created = DateTime.Now },
                new Product() { ProductId = 14, BuyPrice = 160, SellPrice = 370, ProdCatId = 4, Manufacturer = "Razer", Stock = 10, Description = "Razer Huntsman Tournament Edition", Created = DateTime.Now },
                new Product() { ProductId = 15, BuyPrice = 100, SellPrice = 220, ProdCatId = 4, Manufacturer = "Logitech", Stock = 10, Description = "Logitech G502 HERO", Created = DateTime.Now },
                new Product() { ProductId = 16, BuyPrice = 25, SellPrice = 50, ProdCatId = 5, Manufacturer = "Corsair", Stock = 10, Description = "Corsair Vengeance LPX 16 GB", Created = DateTime.Now },
                new Product() { ProductId = 17, BuyPrice = 25, SellPrice = 50, ProdCatId = 5, Manufacturer = "G.Skill", Stock = 10, Description = "G.Skill Ripjaws V 32 GB", Created = DateTime.Now },
                new Product() { ProductId = 18, BuyPrice = 25, SellPrice = 50, ProdCatId = 5, Manufacturer = "Silicon Power", Stock = 10, Description = "Silicon Power GAMING 16 GB", Created = DateTime.Now },
                new Product() { ProductId = 19, BuyPrice = 25, SellPrice = 50, ProdCatId = 5, Manufacturer = "EAMGROUP", Stock = 10, Description = "EAMGROUP T-Force Vulcan Z 16GB", Created = DateTime.Now },
                new Product() { ProductId = 20, BuyPrice = 25, SellPrice = 50, ProdCatId = 5, Manufacturer = "G.Skill", Stock = 10, Description = "G.Skill Trident Z Neo 32 GB", Created = DateTime.Now },
                 new Product() { ProductId = 21, BuyPrice = 100, SellPrice = 200, ProdCatId = 4, Manufacturer = "Razer", Stock = 10, Description = "Basilisk", Created = DateTime.Now }

                );


            modelBuilder.Entity<Customer>().HasData(
                new Customer() { CustomerId = 1, Username = "test1", FirstName = "Charles-Etienne", LastName = "Gauthier", Email = "charles-etienne.gauthier@outlook.com", PhoneNumber = "8199999999", Province = "QC", CreditCard = "8888888888888888" },
                new Customer() { CustomerId = 2, Username = "test2", FirstName = "Jeremy", LastName = "Iyayumeqqch", Email = "jeremy@outlook.com", PhoneNumber = "8199999998", Province = "QC", CreditCard = "8888888888888889" },
                new Customer() { CustomerId = 3, Username = "test3" , FirstName = "Ayon", LastName = "Ghosh", Email = "ayon@outlook.com", PhoneNumber = "8199999997", Province = "QC", CreditCard = "8888888888888887" });

            modelBuilder.Entity<ShoppingCart>().HasData(
                new ShoppingCart() { CustomerId = 1, ShoppingCartId = 1, DateCreated = new DateTime(2022, 12, 1) });

            modelBuilder.Entity<Order>().HasData(new Order() { OrderId = 1, CustomerId = 2, DateCreated = new DateTime(2022, 11, 23), DateFufilled = new DateTime(2022, 11, 24), Total = 1000, Taxes = 15 });

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem() { OrderId = 1, OrderItemId = 1, ProductId = 1, Quantity = 1, Price = 2000 },
                new OrderItem() { OrderId = 1, OrderItemId = 2, ProductId = 2, Quantity = 1, Price = 3000 },
                new OrderItem() { OrderId = 1, OrderItemId = 3, ProductId = 3, Quantity = 1, Price = 109 });

            modelBuilder.Entity<CartItem>().HasData(
                new CartItem() { CartItemId = 1, ShoppingCartId = 1, ProductId = 1, Quantity = 3, Price = 2000 },
                new CartItem() { CartItemId = 2, ShoppingCartId = 1, ProductId = 2, Quantity = 1, Price = 3000 }

                );






            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
