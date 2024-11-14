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
        public DbSet<HolHotel> Hotels { get; set; }
        public DbSet<HolImage> Images { get; set; }
        public DbSet<HolPrice> Prices { get; set; }
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
              .Entity<HolPrice>()
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
            
            base.OnModelCreating(modelBuilder);

        }
    }
}
