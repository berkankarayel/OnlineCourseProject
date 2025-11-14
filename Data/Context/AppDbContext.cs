using Microsoft.EntityFrameworkCore;

namespace OnlineCourseApi.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet'ler buraya eklenecek

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API konfigürasyonları buraya eklenecek
    }
}
