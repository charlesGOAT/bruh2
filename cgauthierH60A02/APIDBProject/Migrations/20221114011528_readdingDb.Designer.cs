﻿// <auto-generated />
using System;
using APIDBProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace H60AssignmentDB_cgauthier.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20221114011528_readdingDb")]
    partial class readdingDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ModelsLibrary.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"), 1L, 1);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasKey("CartItemId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("CartItems");

                    b.HasData(
                        new
                        {
                            CartItemId = 1,
                            Price = 2000m,
                            ProductId = 1,
                            Quantity = 3,
                            ShoppingCartId = 1
                        },
                        new
                        {
                            CartItemId = 2,
                            Price = 3000m,
                            ProductId = 2,
                            Quantity = 1,
                            ShoppingCartId = 1
                        });
                });

            modelBuilder.Entity("ModelsLibrary.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("CreditCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            CreditCard = "8888888888888888",
                            Email = "charles-etienne.gauthier@outlook.com",
                            FirstName = "Charles-Etienne",
                            LastName = "Gauthier",
                            PhoneNumber = "8199999999",
                            Province = "QC",
                            Username = "test1"
                        },
                        new
                        {
                            CustomerId = 2,
                            CreditCard = "8888888888888889",
                            Email = "jeremy@outlook.com",
                            FirstName = "Jeremy",
                            LastName = "Iyayumeqqch",
                            PhoneNumber = "8199999998",
                            Province = "QC",
                            Username = "test2"
                        },
                        new
                        {
                            CustomerId = 3,
                            CreditCard = "8888888888888887",
                            Email = "ayon@outlook.com",
                            FirstName = "Ayon",
                            LastName = "Ghosh",
                            PhoneNumber = "8199999997",
                            Province = "QC",
                            Username = "test3"
                        });
                });

            modelBuilder.Entity("ModelsLibrary.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFufilled")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Taxes")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            CustomerId = 2,
                            DateCreated = new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateFufilled = new DateTime(2022, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Taxes = 15m,
                            Total = 1000m
                        });
                });

            modelBuilder.Entity("ModelsLibrary.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            OrderItemId = 1,
                            OrderId = 1,
                            Price = 2000m,
                            ProductId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 2,
                            OrderId = 1,
                            Price = 3000m,
                            ProductId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 3,
                            OrderId = 1,
                            Price = 109m,
                            ProductId = 3,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("ModelsLibrary.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<decimal?>("BuyPrice")
                        .IsRequired()
                        .HasColumnType("numeric(8,2)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)");

                    b.Property<int>("ProdCatId")
                        .HasColumnType("int");

                    b.Property<decimal?>("SellPrice")
                        .IsRequired()
                        .HasColumnType("numeric(8,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex(new[] { "ProdCatId" }, "IX_Product_ProdCatId");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            BuyPrice = 2000m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9051),
                            Description = "Intel 9900k I9",
                            Manufacturer = "Intel",
                            ProdCatId = 2,
                            SellPrice = 3000m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 2,
                            BuyPrice = 1000m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9077),
                            Description = "Asus ROG MAXIMUS",
                            Manufacturer = "Asus",
                            ProdCatId = 3,
                            SellPrice = 2000m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 3,
                            BuyPrice = 150m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9079),
                            Description = "Ryzen 3",
                            Manufacturer = "AMD",
                            ProdCatId = 2,
                            SellPrice = 350m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 4,
                            BuyPrice = 350m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9083),
                            Description = "Intel Core i9-12900K",
                            Manufacturer = "Intel",
                            ProdCatId = 2,
                            SellPrice = 570m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 5,
                            BuyPrice = 250m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9084),
                            Description = "AMD Ryzen 7 5700X",
                            Manufacturer = "AMD",
                            ProdCatId = 2,
                            SellPrice = 350m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 6,
                            BuyPrice = 150m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9086),
                            Description = "AMD Ryzen 5 5500",
                            Manufacturer = "AMD",
                            ProdCatId = 2,
                            SellPrice = 270m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 7,
                            BuyPrice = 250m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9087),
                            Description = "Asus TUF GAMING X570-PLUS",
                            Manufacturer = "Asus",
                            ProdCatId = 3,
                            SellPrice = 380m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 8,
                            BuyPrice = 260m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9089),
                            Description = "MSI B550-A PRO",
                            Manufacturer = "MSI",
                            ProdCatId = 3,
                            SellPrice = 375m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 9,
                            BuyPrice = 200m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9090),
                            Description = "Gigabyte B660M DS3H DDR4",
                            Manufacturer = "Gigabyte",
                            ProdCatId = 3,
                            SellPrice = 330m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 10,
                            BuyPrice = 190m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9092),
                            Description = "MSI B450 TOMAHAWK MAX",
                            Manufacturer = "MSI",
                            ProdCatId = 3,
                            SellPrice = 335m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 11,
                            BuyPrice = 100m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9094),
                            Description = "HP HyperX Cloud II",
                            Manufacturer = "HP",
                            ProdCatId = 4,
                            SellPrice = 200m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 12,
                            BuyPrice = 170m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9095),
                            Description = "Logitech Pro X 7.1",
                            Manufacturer = "Logitech",
                            ProdCatId = 4,
                            SellPrice = 300m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 13,
                            BuyPrice = 50m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9097),
                            Description = "Corsair K55",
                            Manufacturer = "Corsair",
                            ProdCatId = 4,
                            SellPrice = 95m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 14,
                            BuyPrice = 160m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9099),
                            Description = "Razer Huntsman Tournament Edition",
                            Manufacturer = "Razer",
                            ProdCatId = 4,
                            SellPrice = 370m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 15,
                            BuyPrice = 100m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9102),
                            Description = "Logitech G502 HERO",
                            Manufacturer = "Logitech",
                            ProdCatId = 4,
                            SellPrice = 220m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 16,
                            BuyPrice = 25m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9104),
                            Description = "Corsair Vengeance LPX 16 GB",
                            Manufacturer = "Corsair",
                            ProdCatId = 5,
                            SellPrice = 50m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 17,
                            BuyPrice = 25m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9105),
                            Description = "G.Skill Ripjaws V 32 GB",
                            Manufacturer = "G.Skill",
                            ProdCatId = 5,
                            SellPrice = 50m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 18,
                            BuyPrice = 25m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9107),
                            Description = "Silicon Power GAMING 16 GB",
                            Manufacturer = "Silicon Power",
                            ProdCatId = 5,
                            SellPrice = 50m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 19,
                            BuyPrice = 25m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9109),
                            Description = "EAMGROUP T-Force Vulcan Z 16GB",
                            Manufacturer = "EAMGROUP",
                            ProdCatId = 5,
                            SellPrice = 50m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 20,
                            BuyPrice = 25m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9110),
                            Description = "G.Skill Trident Z Neo 32 GB",
                            Manufacturer = "G.Skill",
                            ProdCatId = 5,
                            SellPrice = 50m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 21,
                            BuyPrice = 100m,
                            Created = new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9112),
                            Description = "Basilisk",
                            Manufacturer = "Razer",
                            ProdCatId = 4,
                            SellPrice = 200m,
                            Stock = 10
                        });
                });

            modelBuilder.Entity("ModelsLibrary.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProdCat")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.HasKey("CategoryId");

                    b.ToTable("ProductCategory", (string)null);

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ProdCat = "Unknown"
                        },
                        new
                        {
                            CategoryId = 2,
                            ProdCat = "CPU"
                        },
                        new
                        {
                            CategoryId = 3,
                            ProdCat = "Motherboard"
                        },
                        new
                        {
                            CategoryId = 4,
                            ProdCat = "Peripherals"
                        },
                        new
                        {
                            CategoryId = 5,
                            ProdCat = "Ram"
                        });
                });

            modelBuilder.Entity("ModelsLibrary.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingCartId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("ShoppingCartId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");

                    b.HasData(
                        new
                        {
                            ShoppingCartId = 1,
                            CustomerId = 1,
                            DateCreated = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ModelsLibrary.CartItem", b =>
                {
                    b.HasOne("ModelsLibrary.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelsLibrary.ShoppingCart", "ShoppingCart")
                        .WithMany("CartItems")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("ModelsLibrary.Order", b =>
                {
                    b.HasOne("ModelsLibrary.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ModelsLibrary.OrderItem", b =>
                {
                    b.HasOne("ModelsLibrary.Order", "Order")
                        .WithMany("OrdersItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelsLibrary.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ModelsLibrary.Product", b =>
                {
                    b.HasOne("ModelsLibrary.ProductCategory", "ProdCat")
                        .WithMany("Products")
                        .HasForeignKey("ProdCatId")
                        .IsRequired()
                        .HasConstraintName("FK_Product_ProductCategory");

                    b.Navigation("ProdCat");
                });

            modelBuilder.Entity("ModelsLibrary.ShoppingCart", b =>
                {
                    b.HasOne("ModelsLibrary.Customer", "Customer")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("ModelsLibrary.ShoppingCart", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ModelsLibrary.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("ModelsLibrary.Order", b =>
                {
                    b.Navigation("OrdersItems");
                });

            modelBuilder.Entity("ModelsLibrary.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ModelsLibrary.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ModelsLibrary.ShoppingCart", b =>
                {
                    b.Navigation("CartItems");
                });
#pragma warning restore 612, 618
        }
    }
}