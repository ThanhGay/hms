using HMS.Bill.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Bill.Infrastructures
{
    public class BillBookingDbContext : DbContext
    {
        public DbSet<BillBillBooking> BillBookings { get; set; }
        public DbSet<BillDiscount > Discounts { get; set; }
        public DbSet<BillPayment > Payments { get; set; }
        public DbSet<BillBillBookingRoom> BillBookingRooms { get; set; }



        public BillBookingDbContext(DbContextOptions<BillBookingDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BillBillBookingRoom>()
                .HasOne<BillBillBooking>()
                .WithMany()
                .HasForeignKey(e => e.BillID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<BillBillBooking>()
                .HasOne<BillDiscount>()
                .WithOne()
                .HasForeignKey<BillBillBooking>(e => e.DiscountID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

        }
    }
}
