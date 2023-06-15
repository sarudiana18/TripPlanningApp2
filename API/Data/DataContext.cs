using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, 
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }

        public DbSet<AtractieTuristica> AtractiiTuristice { get; set; }
        public DbSet<Hotel> Hoteluri { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<City>(
                entity => {
                    entity.ToTable(name: "cities");
                    entity.HasOne(x=> x.Country).WithMany().HasForeignKey(x=> x.Country_Id);
                    entity.HasOne(x=> x.State).WithMany().HasForeignKey(x=> x.State_Id).OnDelete(DeleteBehavior.NoAction);
                    entity.HasMany(x=> x.AtractiiTuristice).WithOne(x=> x.City).HasForeignKey(x=> x.CityId);
                    entity.HasMany(x=> x.Hoteluri).WithOne(x=> x.City).HasForeignKey(x=> x.CityId); 
                }
            );
    
            builder.Entity<Country>(
                entity => {
                    entity.ToTable(name: "countries");
                }
            );

            builder.Entity<State>(
                entity => {
                    entity.ToTable(name: "states");
                    entity.Property("Id").ValueGeneratedNever();
                    entity.HasOne(x=> x.Country).WithMany().HasForeignKey(x=> x.Country_Id);
                }
            );
            builder.Entity<Review>()
                .HasOne(x=> x.CreatedByUser)
                .WithMany(x=> x.Reviews)
                .HasForeignKey(x=> x.CreatedBy);

            builder.Entity<AtractieTuristica>();

            builder.Entity<Hotel>()
                .HasMany(x=> x.Reviews)
                .WithOne(x=> x.Hotel)
                .HasForeignKey(x=> x.HotelId);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            builder.Properties<DateOnly?>()
                .HaveConversion<NullableDateOnlyConverter>()
                .HaveColumnType("date");
        }
    }
}