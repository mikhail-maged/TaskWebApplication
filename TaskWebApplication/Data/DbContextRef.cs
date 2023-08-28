using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskWebApplication.Models;

namespace TaskWebApplication.Data
{
    public class DbContextRef :IdentityDbContext<AppUser>
    {
        public DbContextRef(DbContextOptions<DbContextRef> options):base(options)
        {
            
        }

       public DbSet<ToDoListItems> ToDoListItems { get; set;}
    }
}
