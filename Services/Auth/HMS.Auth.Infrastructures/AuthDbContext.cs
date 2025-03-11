using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Auth.Domain;
using Microsoft.EntityFrameworkCore;

namespace HMS.Auth.Infrastructures
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<AuthPermission> AuthPermissions { get; set; }
        public DbSet<AuthCustomer> AuthCustomers { get; set; }
        public DbSet<AuthReceptionist> AuthReceptionists { get; set; }
        public DbSet<AuthRole> AuthRoles { get; set; }
        public DbSet<AuthRolePermission> AuthRolesPermissions { get; set; }
        public DbSet<AuthVoucher> AuthVouchers { get; set; }
        public DbSet<AuthCustomerVoucher> AuthCustomerVouchers { get; set; }
        public DbSet<AuthFavouriteRoom> AuthFavouriteRooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AuthUser>()
                .HasOne<AuthRole>()
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<AuthRolePermission>()
                .HasOne<AuthRole>()
                .WithMany()
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<AuthCustomer>()
                .HasOne<AuthUser>()
                .WithOne()
                .HasForeignKey<AuthCustomer>(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<AuthReceptionist>()
                .HasOne<AuthUser>()
                .WithOne()
                .HasForeignKey<AuthReceptionist>(a => a.ReceptionistId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<AuthRolePermission>()
                .HasOne<AuthPermission>()
                .WithMany()
                .HasForeignKey(a => a.PermissonKey)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<AuthCustomerVoucher>()
                .HasOne<AuthCustomer>()
                .WithMany()
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<AuthCustomerVoucher>()
                .HasOne<AuthVoucher>()
                .WithMany()
                .HasForeignKey(a => a.VoucherId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<AuthCustomerVoucher>()
                .HasKey(e => new { e.VoucherId, e.CustomerId });
            modelBuilder.Entity<AuthFavouriteRoom>()
                .HasOne<AuthCustomer>()
                .WithMany()
                .HasForeignKey(f => f.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
    }
}
