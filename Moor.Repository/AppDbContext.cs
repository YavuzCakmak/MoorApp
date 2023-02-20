using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;

namespace Moor.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //public DbSet<AgencyEntity> Agency { get; set; }
        public DbSet<CarEntity> Car { get; set; }
        public DbSet<CarParameterEntity> CarParameter { get; set; }
        //public DbSet<CityEntity> City { get; set; }
        //public DbSet<CountryEntity> Country { get; set; }
        //public DbSet<CountyEntity> CountyEntity { get; set; }
        //public DbSet<DisctrictEntity> District { get; set; }
        //public DbSet<DriverCarEntity> DriverCar { get; set; }
        //public DbSet<DriverEntity> Driver { get; set; }
        //public DbSet<PriceEntity> Price { get; set; }
        //public DbSet<StaffEntity> Staff { get; set; }
        //public DbSet<TransferEntity> Transfer { get; set; }
        //public DbSet<TravellerEntity> Traveller { get; set; }
        //public DbSet<UserEntity> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
