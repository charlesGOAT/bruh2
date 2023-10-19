using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H60AssignmentDB_cgauthier.Migrations
{
    public partial class readdingDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditCard = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdCat = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFufilled = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdCatId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    Manufacturer = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    SellPrice = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory",
                        column: x => x.ProdCatId,
                        principalTable: "ProductCategory",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CreditCard", "Email", "FirstName", "LastName", "PhoneNumber", "Province", "Username" },
                values: new object[,]
                {
                    { 1, "8888888888888888", "charles-etienne.gauthier@outlook.com", "Charles-Etienne", "Gauthier", "8199999999", "QC", "test1" },
                    { 2, "8888888888888889", "jeremy@outlook.com", "Jeremy", "Iyayumeqqch", "8199999998", "QC", "test2" },
                    { 3, "8888888888888887", "ayon@outlook.com", "Ayon", "Ghosh", "8199999997", "QC", "test3" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryID", "Image", "ProdCat" },
                values: new object[,]
                {
                    { 1, null, "Unknown" },
                    { 2, null, "CPU" },
                    { 3, null, "Motherboard" },
                    { 4, null, "Peripherals" },
                    { 5, null, "Ram" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DateCreated", "DateFufilled", "Taxes", "Total" },
                values: new object[] { 1, 2, new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 15m, 1000m });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductID", "BuyPrice", "Created", "Description", "Image", "Manufacturer", "ProdCatId", "SellPrice", "Stock" },
                values: new object[,]
                {
                    { 1, 2000m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9051), "Intel 9900k I9", null, "Intel", 2, 3000m, 10 },
                    { 2, 1000m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9077), "Asus ROG MAXIMUS", null, "Asus", 3, 2000m, 10 },
                    { 3, 150m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9079), "Ryzen 3", null, "AMD", 2, 350m, 10 },
                    { 4, 350m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9083), "Intel Core i9-12900K", null, "Intel", 2, 570m, 10 },
                    { 5, 250m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9084), "AMD Ryzen 7 5700X", null, "AMD", 2, 350m, 10 },
                    { 6, 150m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9086), "AMD Ryzen 5 5500", null, "AMD", 2, 270m, 10 },
                    { 7, 250m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9087), "Asus TUF GAMING X570-PLUS", null, "Asus", 3, 380m, 10 },
                    { 8, 260m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9089), "MSI B550-A PRO", null, "MSI", 3, 375m, 10 },
                    { 9, 200m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9090), "Gigabyte B660M DS3H DDR4", null, "Gigabyte", 3, 330m, 10 },
                    { 10, 190m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9092), "MSI B450 TOMAHAWK MAX", null, "MSI", 3, 335m, 10 },
                    { 11, 100m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9094), "HP HyperX Cloud II", null, "HP", 4, 200m, 10 },
                    { 12, 170m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9095), "Logitech Pro X 7.1", null, "Logitech", 4, 300m, 10 },
                    { 13, 50m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9097), "Corsair K55", null, "Corsair", 4, 95m, 10 },
                    { 14, 160m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9099), "Razer Huntsman Tournament Edition", null, "Razer", 4, 370m, 10 },
                    { 15, 100m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9102), "Logitech G502 HERO", null, "Logitech", 4, 220m, 10 },
                    { 16, 25m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9104), "Corsair Vengeance LPX 16 GB", null, "Corsair", 5, 50m, 10 },
                    { 17, 25m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9105), "G.Skill Ripjaws V 32 GB", null, "G.Skill", 5, 50m, 10 },
                    { 18, 25m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9107), "Silicon Power GAMING 16 GB", null, "Silicon Power", 5, 50m, 10 },
                    { 19, 25m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9109), "EAMGROUP T-Force Vulcan Z 16GB", null, "EAMGROUP", 5, 50m, 10 },
                    { 20, 25m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9110), "G.Skill Trident Z Neo 32 GB", null, "G.Skill", 5, 50m, 10 },
                    { 21, 100m, new DateTime(2022, 11, 13, 20, 15, 27, 974, DateTimeKind.Local).AddTicks(9112), "Basilisk", null, "Razer", 4, 200m, 10 }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "ShoppingCartId", "CustomerId", "DateCreated" },
                values: new object[] { 1, 1, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "CartItemId", "Price", "ProductId", "Quantity", "ShoppingCartId" },
                values: new object[,]
                {
                    { 1, 2000m, 1, 3, 1 },
                    { 2, 3000m, 2, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 2000m, 1, 1 },
                    { 2, 1, 3000m, 2, 1 },
                    { 3, 1, 109m, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProdCatId",
                table: "Product",
                column: "ProdCatId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CustomerId",
                table: "ShoppingCarts",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ProductCategory");
        }
    }
}
