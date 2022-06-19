using Microsoft.EntityFrameworkCore;

namespace bARTTest; 

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Incident> Incidents { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Contact> Contacts { get; set; }

}