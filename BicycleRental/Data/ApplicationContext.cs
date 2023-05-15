using BicycleRental.Data.Enitities;
using BicycleRental.Data.Enitities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BicycleRental.Data
{
    public class ApplicationContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Bike> Bikes { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) 
        {
            
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSnakeCaseNamingConvention();
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(x => x.Transactions).WithOne(x => x.User).HasForeignKey(x => x.UserId);
               
            });

            builder.Entity<Transaction>(entity=>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Bike).WithOne(x => x.Transaction).HasForeignKey<Transaction>(x => x.BikeId);

            });
            builder.Entity<Bike>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateStatistics();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateStatistics()
        {
            var delete = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);
            foreach (var entityEntry in delete)
            {
                if (entityEntry.Entity is ITrackable baseEntity)
                {
                    baseEntity.DeletedAt = DateTime.UtcNow;
                }
            }

            var update = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in update)
            {
                if (entityEntry.Entity is ITrackable baseEntity)
                {
                    baseEntity.UpdatedAt = DateTime.UtcNow;
                }
            }

            var created = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in created)
            {
                if (entityEntry.Entity is ITrackable baseEntity)
                {
                    baseEntity.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
