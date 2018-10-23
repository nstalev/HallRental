

namespace HallRental.Data
{

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HallRentalDbContext : IdentityDbContext<User>
    {
        public HallRentalDbContext(DbContextOptions<HallRentalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>()
                .HasOne(e => e.Hall)
                .WithMany(h => h.Events)
                .HasForeignKey(e => e.HallId);

            builder.Entity<Event>()
                .HasOne(e => e.Tenant)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.TenantId);

            base.OnModelCreating(builder);
           

        }
    }
}
