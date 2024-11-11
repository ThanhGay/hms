using HMS.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Auth.Infrastructures
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<AuthManager> AuthManagers { get; set; }
        public DbSet<AuthCustomer> AuthCustomers { get; set; }
        public DbSet<AuthReceptionist> AuthReceptionists { get; set; }
        public DbSet<AuthRole> AuthRoles { get; set; }
        public DbSet<AuthRolePermission> AuthRolesPermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthUser>()
                .HasOne<AuthRole>()
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AuthRolePermission>()
                .HasOne<AuthRole>()
                .WithMany()
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AuthCustomer>()
                .HasOne<AuthUser>()
                .WithOne()
                .HasForeignKey<AuthCustomer>(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AuthReceptionist>()
                .HasOne<AuthUser>()
                .WithOne()
                .HasForeignKey<AuthReceptionist>(a => a.ReceptionistId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AuthManager>()
                .HasOne<AuthUser>()
                .WithOne()
                .HasForeignKey<AuthManager>(a => a.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
    }
}
