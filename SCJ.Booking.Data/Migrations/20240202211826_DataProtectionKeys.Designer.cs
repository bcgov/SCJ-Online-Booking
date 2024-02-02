﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SCJ.Booking.Data;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240202211826_DataProtectionKeys")]
    partial class DataProtectionKeys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.26");

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FriendlyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Xml")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.BookingHistory", b =>
                {
                    b.Property<string>("SmGovUserGuid")
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<long>("ContainerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("SmGovUserGuid", "ContainerId", "Timestamp");

                    b.ToTable("BookingHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
