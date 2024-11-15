using HMS.Hol.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.Infrastructures
{
    public class HotelDbContext : DbContext
    {
        public DbSet<HolBillBooking> BillBookings { get; set; }
        public DbSet<HolBillBooking_Room> BillBooking_Rooms { get; set; }
        public DbSet<HolCharge> Charges { get; set; }
        public DbSet<HolDefaultPrice> DefaultPrices { get; set; }
        public DbSet<HolDiscount> Discounts { get; set; }
        public DbSet<HolHotel> Hotels { get; set; }
        public DbSet<HolImage> Images { get; set; }
        public DbSet<HolSubPrice> SubPrices { get; set; }
        public DbSet<HolRoom> Rooms { get; set; }
        public DbSet<HolRoomDetail> RoomDetails { get; set; }
        public DbSet<HolRoomType> RoomTypes { get; set; }
        public DbSet<HolRoomType_RoomDetail> RoomType_RoomDetails { get; set; }

        public HotelDbContext(DbContextOptions <HotelDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<HolImage>()
              .HasOne<HolRoom>()
              .WithMany()
              .HasForeignKey(e => e.RoomId)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
              .Entity<HolRoom>()
              .HasOne<HolHotel>()
              .WithMany()
              .HasForeignKey(e => e.HotelId)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
              .Entity<HolRoom>()
              .HasOne<HolRoomType>()
              .WithMany()
              .HasForeignKey(e => e.RoomTypeId)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
              .Entity<HolSubPrice>()
              .HasOne<HolRoomType>()
              .WithMany()
              .HasForeignKey(e => e.RoomTypeID)
              .OnDelete(DeleteBehavior.Restrict);


            modelBuilder
                .Entity<HolRoomType_RoomDetail>()
                .HasKey(e =>new{ e.RoomDetailID,e.RoomTypeID});
            modelBuilder
              .Entity<HolRoomType_RoomDetail>()
              .HasOne<HolRoomType>()
              .WithMany()
              .HasForeignKey(e => e.RoomTypeID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
              .Entity<HolRoomType_RoomDetail>()
              .HasOne<HolRoomDetail>()
              .WithMany()
              .HasForeignKey(e => e.RoomDetailID)
              .OnDelete(DeleteBehavior.Restrict);


            modelBuilder
                .Entity<HolDefaultPrice>()
                .HasOne<HolRoomType>()
                .WithMany()
                .HasForeignKey(e=>e.RoomTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<HolBillBooking_Room>()
               .HasKey(e => new { e.BillID, e.RoomID });

            modelBuilder
                .Entity<HolBillBooking_Room>()
                .HasOne<HolBillBooking>()
                .WithMany()
                .HasForeignKey(e=>e.BillID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<HolBillBooking_Room>()
                .HasOne<HolRoom>()
                .WithMany()
                .HasForeignKey(e=> e.RoomID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<HolCharge>()
                .HasOne<HolBillBooking> ()
                .WithMany()
                .HasForeignKey(e=> e.ChargeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<HolBillBooking>()
                .HasOne<HolDiscount>()
                .WithOne()
                .HasForeignKey<HolDiscount>(e=>e.DiscountID)
                .OnDelete(DeleteBehavior.Restrict);



            base.OnModelCreating(modelBuilder);

        }
    }
}
