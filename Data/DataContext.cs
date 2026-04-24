using InterfazdeAdministración_SistemadeLibrería.Models;
using Microsoft.EntityFrameworkCore;

namespace InterfazdeAdministración_SistemadeLibrería.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<book>().ToTable("Books");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Loan>().ToTable("Loans");
    }
}