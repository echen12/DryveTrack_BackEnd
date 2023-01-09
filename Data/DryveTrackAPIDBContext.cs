using DryveTrack_BackEnd.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DryveTrack_BackEnd.Data
{
    public class DryveTrackAPIDBContext : IdentityUserContext<IdentityUser>
    {
        public DryveTrackAPIDBContext (DbContextOptions<DryveTrackAPIDBContext> options): base(options)
        {

        }

        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<UserVehicle> UserVehicle { get; set; }
        public DbSet<VehicleInsurance> VehicleInsurances { get; set; }
        public DbSet<Odometer> Odometer { get; set; }
        public DbSet<VehicleOdometer> VehicleOdometer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
