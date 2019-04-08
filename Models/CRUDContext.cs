using Microsoft.EntityFrameworkCore;
 
namespace crud.Models
{
    public class CRUDContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public CRUDContext(DbContextOptions<CRUDContext> options) : base(options) { }
    }
}