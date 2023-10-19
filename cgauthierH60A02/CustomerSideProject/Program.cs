using CustomerSideProject.Data;
using ModelsLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

//using H60AssignmentDB_cgauthier.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});




var connectionIdentityString = builder.Configuration.GetConnectionString("H60AssignmentDB_cgauthierContextConnection");
builder.Services.AddDbContext<H60AssignmentDB_cgauthierContext>(x => x.UseSqlServer(connectionIdentityString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
      .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<H60AssignmentDB_cgauthierContext>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {

    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<CurrentSession>();

builder.Services.AddScoped<IStoreRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IStoreRepository<ProductCategory>, ProductCategoriesRepository>();
builder.Services.AddScoped<IStoreRepository<Customer>, CustomerRepository>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseSession();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
