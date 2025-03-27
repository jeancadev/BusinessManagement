using BusinessManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagement.Infrastructure.Persistence
{
    public class BusinessManagementDbContext : DbContext
    {
        public BusinessManagementDbContext(DbContextOptions<BusinessManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<AppUser> Users => Set<AppUser>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Semilla de usuarios con valores fijos (estaticos) en HasData
            // AppUser es la entidad con { Username, HashedPassword, Role.}
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = new Guid("a73e7d99-5d1b-44cd-b38d-31e9a726f0b7"), // Valor fijo
                    Username = "admin",
                    HashedPassword = "$2a$11$QoGpjBRRFdALcvpS1STwCues386.ao6sIdHq3SAo/4k3UE3/i.dFe", // Hash de "1234" (fijo)
                    Role = "Admin",
                    CreatedAt = new DateTime(2025, 3, 10, 0, 0, 0, DateTimeKind.Utc)
                },
                new AppUser
                {
                    Id = new Guid("b55e7d99-7a1b-44cd-b38d-31e9a726f0b8"), // Valor fijo
                    Username = "it",
                    HashedPassword = "$2a$11$cTTeqtMiGtmyob0Ku2Rx5eQGbVurEKphzvWmrVC3dnWivAnWLX0za", // Hash de "9999"
                    Role = "IT",
                    CreatedAt = new DateTime(2025, 3, 10, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
