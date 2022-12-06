﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TripManagement.Infrastructure.Persistence;

#nullable disable

namespace TripManagement.Infrastructure.Migrations
{
    [DbContext(typeof(TripManagementContext))]
    partial class TripManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TripManagement.Domain.CitiesAggregate.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("City");

                    b.HasData(
                        new
                        {
                            Id = new Guid("41846153-8b22-4e1e-93cd-c6ec842cca43"),
                            Active = true,
                            Name = "Sabadell",
                            SoftDeleted = false
                        },
                        new
                        {
                            Id = new Guid("16487808-50bf-480b-a4bf-320e326ffb98"),
                            Active = true,
                            Name = "Barcelona",
                            SoftDeleted = false
                        });
                });

            modelBuilder.Entity("TripManagement.Domain.DriversAggregate.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("TripManagement.Domain.DriversAggregate.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Driver");
                });

            modelBuilder.Entity("TripManagement.Domain.TripsAggregate.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("TripManagement.Domain.TripsAggregate.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CreditsCost")
                        .HasColumnType("int");

                    b.Property<Guid>("DestinationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Kilometers")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("OriginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PickUp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("TripStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.HasIndex("DriverId");

                    b.HasIndex("OriginId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("TripManagement.Domain.DriversAggregate.Driver", b =>
                {
                    b.HasOne("TripManagement.Domain.DriversAggregate.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("TripManagement.Domain.Common.Coordinates", "CurrentCoordinates", b1 =>
                        {
                            b1.Property<Guid>("DriverId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Latitude")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Latitude");

                            b1.Property<decimal>("Longitude")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Longitude");

                            b1.HasKey("DriverId");

                            b1.ToTable("Driver");

                            b1.WithOwner()
                                .HasForeignKey("DriverId");
                        });

                    b.Navigation("Car");

                    b.Navigation("CurrentCoordinates");
                });

            modelBuilder.Entity("TripManagement.Domain.TripsAggregate.Location", b =>
                {
                    b.HasOne("TripManagement.Domain.CitiesAggregate.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("TripManagement.Domain.Common.Coordinates", "Coordinates", b1 =>
                        {
                            b1.Property<Guid>("LocationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Latitude")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Latitude");

                            b1.Property<decimal>("Longitude")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Longitude");

                            b1.HasKey("LocationId");

                            b1.ToTable("Location");

                            b1.WithOwner()
                                .HasForeignKey("LocationId");
                        });

                    b.Navigation("City");

                    b.Navigation("Coordinates")
                        .IsRequired();
                });

            modelBuilder.Entity("TripManagement.Domain.TripsAggregate.Trip", b =>
                {
                    b.HasOne("TripManagement.Domain.TripsAggregate.Location", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TripManagement.Domain.DriversAggregate.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TripManagement.Domain.TripsAggregate.Location", "Origin")
                        .WithMany()
                        .HasForeignKey("OriginId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("TripManagement.Domain.Common.Coordinates", "CurrentCoordinates", b1 =>
                        {
                            b1.Property<Guid>("TripId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Latitude")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Latitude");

                            b1.Property<decimal>("Longitude")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Longitude");

                            b1.HasKey("TripId");

                            b1.ToTable("Trips");

                            b1.WithOwner()
                                .HasForeignKey("TripId");
                        });

                    b.OwnsOne("TripManagement.Domain.TripsAggregate.UserId", "UserId", b1 =>
                        {
                            b1.Property<Guid>("TripId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("UserId");

                            b1.HasKey("TripId");

                            b1.ToTable("Trips");

                            b1.WithOwner()
                                .HasForeignKey("TripId");
                        });

                    b.Navigation("CurrentCoordinates")
                        .IsRequired();

                    b.Navigation("Destination");

                    b.Navigation("Driver");

                    b.Navigation("Origin");

                    b.Navigation("UserId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
