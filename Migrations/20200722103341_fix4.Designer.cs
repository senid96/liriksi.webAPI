﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using liriksi.WebAPI.EF;

namespace liriksi.WebAPI.Migrations
{
    [DbContext(typeof(LiriksiContext))]
    [Migration("20200722103341_fix4")]
    partial class fix4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("liriksi.Model.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GenreId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("YearRelease");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("liriksi.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("liriksi.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("liriksi.Model.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("liriksi.Model.Performer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArtisticName");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Performer");
                });

            modelBuilder.Entity("liriksi.Model.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlbumId");

                    b.Property<int>("PerformerId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("PerformerId");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("liriksi.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId");

                    b.Property<string>("Email");

                    b.Property<string>("Hash");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Salt");

                    b.Property<bool>("Status");

                    b.Property<string>("Surname");

                    b.Property<int?>("UserTypeId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("liriksi.Model.UsersAlbumRate", b =>
                {
                    b.Property<int>("AlbumId");

                    b.Property<int>("UserId");

                    b.Property<string>("Comment");

                    b.Property<int>("Rate");

                    b.HasKey("AlbumId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersAlbumRates");
                });

            modelBuilder.Entity("liriksi.Model.UsersSongRate", b =>
                {
                    b.Property<int>("SongId");

                    b.Property<int>("UserId");

                    b.Property<string>("Comment");

                    b.Property<int>("Rate");

                    b.HasKey("SongId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersSongRates");
                });

            modelBuilder.Entity("liriksi.Model.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("liriksi.Model.Album", b =>
                {
                    b.HasOne("liriksi.Model.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("liriksi.Model.City", b =>
                {
                    b.HasOne("liriksi.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("liriksi.Model.Song", b =>
                {
                    b.HasOne("liriksi.Model.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("liriksi.Model.Performer", "Performer")
                        .WithMany()
                        .HasForeignKey("PerformerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("liriksi.Model.User", b =>
                {
                    b.HasOne("liriksi.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("liriksi.Model.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId");
                });

            modelBuilder.Entity("liriksi.Model.UsersAlbumRate", b =>
                {
                    b.HasOne("liriksi.Model.Album")
                        .WithMany("UsersAlbumRates")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("liriksi.Model.User")
                        .WithMany("UsersAlbumRates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("liriksi.Model.UsersSongRate", b =>
                {
                    b.HasOne("liriksi.Model.Song")
                        .WithMany("UsersSongRates")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("liriksi.Model.User")
                        .WithMany("UsersSongRates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
