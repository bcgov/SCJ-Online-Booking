﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SCJ.Booking.Data;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FriendlyName")
                        .HasColumnType("text");

                    b.Property<string>("Xml")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.BookingHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BookingLocationName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("CoaCaseType")
                        .HasMaxLength(8)
                        .HasColumnType("text");

                    b.Property<string>("CoaConferenceType")
                        .HasMaxLength(8)
                        .HasColumnType("text");

                    b.Property<string>("CourtLevel")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("text");

                    b.Property<string>("ScFormulaType")
                        .HasMaxLength(8)
                        .HasColumnType("text");

                    b.Property<int?>("ScHearingType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BookingHistory");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.OidcUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<ushort>("CredentialType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UniqueIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UniqueIdentifier", "CredentialType")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.QueuedEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CourtLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ToEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmailQueue");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.ScTrialBookingRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AllocatedSelectionRank")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("AllocatedSelectionTrialStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("BookHearingCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<int>("BookingLocationId")
                        .HasColumnType("integer");

                    b.Property<int>("CaseNumber")
                        .HasColumnType("integer");

                    b.Property<string>("CaseRegistryCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<int>("CaseRegistryId")
                        .HasColumnType("integer");

                    b.Property<decimal>("CeisPhysicalFileId")
                        .HasColumnType("numeric");

                    b.Property<string>("CourtClassCode")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<DateTime>("FairUseBookingPeriodEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FairUseBookingPeriodStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FairUseContactDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FairUseSort")
                        .HasColumnType("integer");

                    b.Property<int>("HearingLength")
                        .HasColumnType("integer");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LotteryBeginTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LotteryPosition")
                        .HasColumnType("integer");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("RequestedByName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("StyleOfCause")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("TrialBookingId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TrialLocationId")
                        .HasColumnType("integer");

                    b.Property<string>("TrialLocationName")
                        .HasColumnType("text");

                    b.Property<DateTime>("TrialPeriodEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TrialPeriodStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "CaseNumber" }, "IX_CaseNumber_Ascending");

                    b.ToTable("ScTrialBookingRequests");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.ScTrialDateSelection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookingRequestId")
                        .HasColumnType("integer");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TrialStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BookingRequestId");

                    b.ToTable("ScTrialDateSelections");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.BookingHistory", b =>
                {
                    b.HasOne("SCJ.Booking.Data.Models.OidcUser", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.ScTrialBookingRequest", b =>
                {
                    b.HasOne("SCJ.Booking.Data.Models.OidcUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.ScTrialDateSelection", b =>
                {
                    b.HasOne("SCJ.Booking.Data.Models.ScTrialBookingRequest", "BookingRequest")
                        .WithMany("TrialDateSelections")
                        .HasForeignKey("BookingRequestId");

                    b.Navigation("BookingRequest");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.OidcUser", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("SCJ.Booking.Data.Models.ScTrialBookingRequest", b =>
                {
                    b.Navigation("TrialDateSelections");
                });
#pragma warning restore 612, 618
        }
    }
}
