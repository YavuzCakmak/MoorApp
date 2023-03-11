using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;

namespace Moor.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        #region Authorize
        public DbSet<PersonnelEntity> Personnel { get; set; }
        public DbSet<RoleEntity> Role { get; set; }
        public DbSet<PersonnelRoleEntity> PersonnelRole { get; set; }
        #endregion

        public DbSet<AgencyEntity> Agency { get; set; }
        public DbSet<NotificationEntity> Notification { get; set; }
        public DbSet<CarEntity> Car { get; set; }
        public DbSet<CarBrandEntity> CarBrand { get; set; }
        public DbSet<CarModelEntity> CarModel { get; set; }
        public DbSet<CarParameterEntity> CarParameter { get; set; }
        public DbSet<CityEntity> City { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        public DbSet<CountyEntity> CountyEntity { get; set; }
        public DbSet<DistrictEntity> District { get; set; }
        public DbSet<DriverCarEntity> DriverCar { get; set; }
        public DbSet<DriverEntity> Driver { get; set; }
        public DbSet<PriceEntity> Price { get; set; }
        public DbSet<TransferEntity> Transfer { get; set; }
        public DbSet<TravellerEntity> Traveller { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
