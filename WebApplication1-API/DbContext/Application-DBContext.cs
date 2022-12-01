
using DataAccessLayers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1_API.DataContext;


public class Application_DBContext : IdentityDbContext 
{
    public Application_DBContext(DbContextOptions<Application_DBContext>
      options  ) :base (options) 
    {

    }
    public DbSet<Empolyee> Empolyee { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Departments> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

