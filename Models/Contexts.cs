using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Models;

public class Contexts(DbContextOptions options) : DbContext(options)
{
    public DbSet<Car>? Cars { get; set; }
    public DbSet<Motorbike>? Motorbikes { get; set; }
    public DbSet<CarTaxAmount>? CarsTaxAmounts { get; set; }
    public DbSet<MotorbikeTaxAmount>? MotorbikeTaxAmounts { get; set; }
}

public class DbContextFactory : IDesignTimeDbContextFactory<Contexts>
{
    public Contexts CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Contexts>();
        //Change the path of Database!
        const string dir = @"Data Source=/Users/oliver/RiderProjects/Project/Models/db/database.db;";
        optionsBuilder.UseSqlite(dir);
        return new Contexts(optionsBuilder.Options);
    }
}