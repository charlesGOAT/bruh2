
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;

namespace CustomerSideProject.Data;

public class H60AssignmentDB_cgauthierContext : IdentityDbContext<IdentityUser>
{
  
    
    public H60AssignmentDB_cgauthierContext(DbContextOptions<H60AssignmentDB_cgauthierContext> options 
       
            
         
          
           )
        : base(options)
    {
       

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole() { Id = "1", Name = "Customer", NormalizedName = "Customer" }, 
            new IdentityRole() { Id = "2", Name = "Clerk", NormalizedName = "Clerk"  }, 
            new IdentityRole() { Id = "3", Name = "Manager", NormalizedName = "Manager" });
      

    }
}
