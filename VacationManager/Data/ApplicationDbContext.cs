using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VacationManager.Models;

namespace VacationManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Team>().HasOne(t => t.Leader).WithOne(u => u.Team).OnDelete(DeleteBehavior.Restrict).HasForeignKey<Team>(m => m.LeaderId);
            modelBuilder.Entity<Vacation>().HasOne(v => v.ApplicationUser).WithMany(u => u.Vacations).HasForeignKey(m => m.ApplicationUserId);
        }
    }
}