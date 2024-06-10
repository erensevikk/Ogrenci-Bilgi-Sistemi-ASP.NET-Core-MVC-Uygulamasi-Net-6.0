﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.DBContext;

#nullable disable

namespace WebApplication2.Migrations
{
    [DbContext(typeof(FinalDBContext))]
    partial class FinalDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplication2.Models.Belgeler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OgrenciId")
                        .HasColumnType("int");

                    b.Property<bool>("basaribelge")
                        .HasColumnType("bit");

                    b.Property<bool>("onurbelge")
                        .HasColumnType("bit");

                    b.Property<bool>("takdirbelge")
                        .HasColumnType("bit");

                    b.Property<bool>("tskbelge")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("OgrenciId");

                    b.ToTable("Belgeler");
                });

            modelBuilder.Entity("WebApplication2.Models.Ders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DersAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ders");
                });

            modelBuilder.Entity("WebApplication2.Models.Dersler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DersId")
                        .HasColumnType("int");

                    b.Property<int>("OgretmenId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Saat")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("DersId");

                    b.HasIndex("OgretmenId");

                    b.ToTable("Dersler");
                });

            modelBuilder.Entity("WebApplication2.Models.Duyurular", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Duyuru")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OgretmenId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.Property<int>("YerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OgretmenId");

                    b.HasIndex("YerId");

                    b.ToTable("Duyurular");
                });

            modelBuilder.Entity("WebApplication2.Models.Notlar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DersId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("NotDegeri")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("OgrenciId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<bool?>("sonuc")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DersId");

                    b.HasIndex("OgrenciId");

                    b.ToTable("Not");
                });

            modelBuilder.Entity("WebApplication2.Models.Ogrenciler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fotograf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SınıfId")
                        .HasColumnType("int");

                    b.Property<string>("VeliTelNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SınıfId");

                    b.ToTable("Ogrenciler");
                });

            modelBuilder.Entity("WebApplication2.Models.Ogretmenler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DersId")
                        .HasColumnType("int");

                    b.Property<string>("Fotograf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DersId");

                    b.ToTable("Ogretmenler");
                });

            modelBuilder.Entity("WebApplication2.Models.SINIFLAR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("sınıf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SINIFLAR");
                });

            modelBuilder.Entity("WebApplication2.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("WebApplication2.Models.Yerler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("mekan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Yerlers");
                });

            modelBuilder.Entity("WebApplication2.Models.Belgeler", b =>
                {
                    b.HasOne("WebApplication2.Models.Ogrenciler", "Ogrenci")
                        .WithMany()
                        .HasForeignKey("OgrenciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ogrenci");
                });

            modelBuilder.Entity("WebApplication2.Models.Dersler", b =>
                {
                    b.HasOne("WebApplication2.Models.Ders", "Ders")
                        .WithMany()
                        .HasForeignKey("DersId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.Ogretmenler", "Ogretmen")
                        .WithMany()
                        .HasForeignKey("OgretmenId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ders");

                    b.Navigation("Ogretmen");
                });

            modelBuilder.Entity("WebApplication2.Models.Duyurular", b =>
                {
                    b.HasOne("WebApplication2.Models.Ogretmenler", "Ogretmen")
                        .WithMany()
                        .HasForeignKey("OgretmenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.Yerler", "Yer")
                        .WithMany()
                        .HasForeignKey("YerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ogretmen");

                    b.Navigation("Yer");
                });

            modelBuilder.Entity("WebApplication2.Models.Notlar", b =>
                {
                    b.HasOne("WebApplication2.Models.Ders", "DersAd")
                        .WithMany()
                        .HasForeignKey("DersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.Ogrenciler", "Ogrenci")
                        .WithMany()
                        .HasForeignKey("OgrenciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DersAd");

                    b.Navigation("Ogrenci");
                });

            modelBuilder.Entity("WebApplication2.Models.Ogrenciler", b =>
                {
                    b.HasOne("WebApplication2.Models.SINIFLAR", "Sınıf")
                        .WithMany()
                        .HasForeignKey("SınıfId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Sınıf");
                });

            modelBuilder.Entity("WebApplication2.Models.Ogretmenler", b =>
                {
                    b.HasOne("WebApplication2.Models.Ders", "DersAd")
                        .WithMany()
                        .HasForeignKey("DersId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DersAd");
                });
#pragma warning restore 612, 618
        }
    }
}
