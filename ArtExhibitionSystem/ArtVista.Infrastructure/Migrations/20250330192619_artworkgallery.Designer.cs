﻿// <auto-generated />
using System;
using ArtVista.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArtVista.Infrastructure.Migrations
{
    [DbContext(typeof(ArtVistaDbContext))]
    [Migration("20250330192619_artworkgallery")]
    partial class artworkgallery
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtVista.Domain.Entities.Artist", b =>
                {
                    b.Property<string>("ArtistID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistID");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.Artwork", b =>
                {
                    b.Property<int>("ArtworkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtworkID"));

                    b.Property<string>("ArtistID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtworkID");

                    b.HasIndex("ArtistID");

                    b.ToTable("Artworks");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.ArtworkGallery", b =>
                {
                    b.Property<int>("ArtworkID")
                        .HasColumnType("int");

                    b.Property<int>("GalleryID")
                        .HasColumnType("int");

                    b.HasKey("ArtworkID", "GalleryID");

                    b.HasIndex("GalleryID");

                    b.ToTable("ArtworkGalleries");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.FavoriteArtwork", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ArtworkID")
                        .HasColumnType("int");

                    b.Property<int?>("ArtworkID1")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ArtworkID");

                    b.HasIndex("ArtworkID");

                    b.HasIndex("ArtworkID1");

                    b.ToTable("FavoriteArtworks");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.Gallery", b =>
                {
                    b.Property<int>("GalleryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GalleryID"));

                    b.Property<string>("ArtistId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GalleryID");

                    b.HasIndex("ArtistId");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.Artwork", b =>
                {
                    b.HasOne("ArtVista.Domain.Entities.Artist", "Artist")
                        .WithMany("Artworks")
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.ArtworkGallery", b =>
                {
                    b.HasOne("ArtVista.Domain.Entities.Artwork", "Artwork")
                        .WithMany("ArtworkGalleries")
                        .HasForeignKey("ArtworkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtVista.Domain.Entities.Gallery", "Gallery")
                        .WithMany("ArtworkGalleries")
                        .HasForeignKey("GalleryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artwork");

                    b.Navigation("Gallery");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.FavoriteArtwork", b =>
                {
                    b.HasOne("ArtVista.Domain.Entities.Artwork", "Artwork")
                        .WithMany()
                        .HasForeignKey("ArtworkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtVista.Domain.Entities.Artwork", null)
                        .WithMany("FavoriteArtworks")
                        .HasForeignKey("ArtworkID1");

                    b.Navigation("Artwork");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.Gallery", b =>
                {
                    b.HasOne("ArtVista.Domain.Entities.Artist", "Artist")
                        .WithMany("Galleries")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.Artist", b =>
                {
                    b.Navigation("Artworks");

                    b.Navigation("Galleries");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.Artwork", b =>
                {
                    b.Navigation("ArtworkGalleries");

                    b.Navigation("FavoriteArtworks");
                });

            modelBuilder.Entity("ArtVista.Domain.Entities.Gallery", b =>
                {
                    b.Navigation("ArtworkGalleries");
                });
#pragma warning restore 612, 618
        }
    }
}
