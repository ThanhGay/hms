﻿// <auto-generated />
using System;
using HMS.Hol.Infrastructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HMS.WebAPI.Migrations.HotelDb
{
    [DbContext(typeof(HotelDbContext))]
    partial class HotelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HMS.Hol.Domain.HolBillBooking", b =>
                {
                    b.Property<int>("BillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillID"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int?>("DiscountID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpectedCheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpectedCheckOut")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Prepayment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ReceptionistID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BillID");

                    b.ToTable("HolBillBooking", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolBillBooking_Charge", b =>
                {
                    b.Property<int>("Booking_ChargeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Booking_ChargeID"));

                    b.Property<int>("BillID")
                        .HasColumnType("int");

                    b.Property<int>("ChargeID")
                        .HasColumnType("int");

                    b.HasKey("Booking_ChargeID");

                    b.HasIndex("BillID");

                    b.HasIndex("ChargeID");

                    b.ToTable("HolBillBooking_Charge", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolBillBooking_Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BillID")
                        .HasColumnType("int");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BillID");

                    b.HasIndex("RoomID");

                    b.ToTable("HolBillBooking_Room", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descreption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("HolCharge", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolDefaultPrice", b =>
                {
                    b.Property<int>("DefaultPriceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DefaultPriceID"));

                    b.Property<decimal>("PricePerHour")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomTypeID")
                        .HasColumnType("int");

                    b.HasKey("DefaultPriceID");

                    b.HasIndex("RoomTypeID");

                    b.ToTable("HolDefaultPrice", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolHotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelId"));

                    b.Property<string>("HotelAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Hotline")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("HotelId");

                    b.ToTable("HolHotel", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolImage", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageID");

                    b.HasIndex("RoomId");

                    b.ToTable("HolImage", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolRoom", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomID"));

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.HasKey("RoomID");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("HolRoom", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolRoomDetail", b =>
                {
                    b.Property<int>("RoomDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomDetailID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("RoomDetailID");

                    b.ToTable("HolRoomDetail", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolRoomType", b =>
                {
                    b.Property<int>("RoomTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomTypeID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomTypeID");

                    b.ToTable("HolRoomType", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolRoomType_RoomDetail", b =>
                {
                    b.Property<int>("RoomDetailID")
                        .HasColumnType("int");

                    b.Property<int>("RoomTypeID")
                        .HasColumnType("int");

                    b.HasKey("RoomDetailID", "RoomTypeID");

                    b.HasIndex("RoomTypeID");

                    b.ToTable("HolRoomType_RoomDetail", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolSubPrice", b =>
                {
                    b.Property<int>("SubPriceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubPriceID"));

                    b.Property<DateTime>("DayEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DayStart")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PricePerHours")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomTypeID")
                        .HasColumnType("int");

                    b.HasKey("SubPriceID");

                    b.HasIndex("RoomTypeID");

                    b.ToTable("HolSubPrice", "hol");
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolBillBooking_Charge", b =>
                {
                    b.HasOne("HMS.Hol.Domain.HolBillBooking", null)
                        .WithMany()
                        .HasForeignKey("BillID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HMS.Hol.Domain.HolCharge", null)
                        .WithMany()
                        .HasForeignKey("ChargeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolBillBooking_Room", b =>
                {
                    b.HasOne("HMS.Hol.Domain.HolBillBooking", null)
                        .WithMany()
                        .HasForeignKey("BillID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HMS.Hol.Domain.HolRoom", null)
                        .WithMany()
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolDefaultPrice", b =>
                {
                    b.HasOne("HMS.Hol.Domain.HolRoomType", null)
                        .WithMany()
                        .HasForeignKey("RoomTypeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolImage", b =>
                {
                    b.HasOne("HMS.Hol.Domain.HolRoom", null)
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolRoom", b =>
                {
                    b.HasOne("HMS.Hol.Domain.HolHotel", null)
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HMS.Hol.Domain.HolRoomType", null)
                        .WithMany()
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolRoomType_RoomDetail", b =>
                {
                    b.HasOne("HMS.Hol.Domain.HolRoomDetail", null)
                        .WithMany()
                        .HasForeignKey("RoomDetailID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HMS.Hol.Domain.HolRoomType", null)
                        .WithMany()
                        .HasForeignKey("RoomTypeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HMS.Hol.Domain.HolSubPrice", b =>
                {
                    b.HasOne("HMS.Hol.Domain.HolRoomType", null)
                        .WithMany()
                        .HasForeignKey("RoomTypeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
