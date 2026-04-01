using Microsoft.EntityFrameworkCore;
using UserProfilesApp.Models;

namespace UserProfilesApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(x => x.UserName).IsUnique();
            entity.HasIndex(x => x.Email).IsUnique();

            entity.HasOne(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<UserProfile>(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.Property(x => x.FirstName).IsRequired();
            entity.Property(x => x.LastName).IsRequired();
        });
    }
}

