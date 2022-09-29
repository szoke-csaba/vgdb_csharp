using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Game>? Games { get; set; }
    public DbSet<Vote>? Votes { get; set; }
    public DbSet<ApplicationUser>? Users { get; set; }
    public DbSet<UserList>? UserLists { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedRoles(builder);
        SeedUsers(builder);
        SeedUserRoles(builder);
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Id = "f1941b48-148a-4982-90ee-4280aed361ed",
                Name = "User", NormalizedName = "USER",
                ConcurrencyStamp = "0ecec47d-6f73-4424-97ca-dc945dd27dc0"
            },
            new IdentityRole()
            {
                Id = "kab4fac1-c546-41de-aebc-a14da6895711",
                Name = "Admin", NormalizedName = "ADMIN",
                ConcurrencyStamp = "0shuj12m-6s73-2386-79aj-zv285go18py0"
            }
        );
    }

    private static void SeedUsers(ModelBuilder builder)
    {
        ApplicationUser user = new()
        {
            Id = "X4v2E07H-k7XQ-QqGo-lcV6-zHdO3mCmulpA",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = "admin@admin.admin",
            NormalizedEmail = "ADMIN@ADMIN.ADMIN"
        };

        PasswordHasher<ApplicationUser> passwordHasher = new();
        user.PasswordHash = passwordHasher.HashPassword(user, "Admin123!");

        builder.Entity<ApplicationUser>().HasData(user);
    }

    private static void SeedUserRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>()
            {
                RoleId = "kab4fac1-c546-41de-aebc-a14da6895711",
                UserId = "X4v2E07H-k7XQ-QqGo-lcV6-zHdO3mCmulpA"
            }
        );
    }
}
