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
            base.OnModelCreating(modelBuilder);
        }
    }
}
