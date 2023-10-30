using Microsoft.EntityFrameworkCore;
using APICrudRestaurant.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define any additional configuration for your tables here (e.g., indexes, relationships).


        modelBuilder.Entity<Food>()
            .Property(f => f.Price)
            .HasColumnType("decimal(18, 2)"); // This specifies a decimal with 18 total digits and 2 decimal places.

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Customer)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CustomerId);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Food)
            .WithMany(f => f.Transactions)
            .HasForeignKey(t => t.FoodId);

        base.OnModelCreating(modelBuilder);
    }
}