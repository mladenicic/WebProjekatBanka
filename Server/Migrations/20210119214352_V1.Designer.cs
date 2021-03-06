﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Models;

namespace Server.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20210119214352_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Server.Models.Banka", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int>("Kapacitet")
                        .HasColumnType("int")
                        .HasColumnName("Kapacitet");

                    b.Property<string>("Naziv")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Naziv");

                    b.Property<int>("Vrednost")
                        .HasColumnType("int")
                        .HasColumnName("Vrednost");

                    b.HasKey("ID");

                    b.ToTable("Banka");
                });

            modelBuilder.Entity("Server.Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int?>("BankaID")
                        .HasColumnType("int");

                    b.Property<string>("DatumRodjenja")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Datum Rodjenja");

                    b.Property<string>("Ime")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Ime");

                    b.Property<string>("Prezime")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Prezime");

                    b.HasKey("ID");

                    b.HasIndex("BankaID");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("Server.Models.Kredit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int?>("BankaID")
                        .HasColumnType("int");

                    b.Property<int>("Broj")
                        .HasColumnType("int")
                        .HasColumnName("Broj");

                    b.Property<string>("DatumIsplate")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Datum Isplate");

                    b.Property<string>("DatumPodizanja")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Datum Podizanja");

                    b.Property<int>("Iznos")
                        .HasColumnType("int")
                        .HasColumnName("Iznos");

                    b.Property<int?>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int>("VracenIznos")
                        .HasColumnType("int")
                        .HasColumnName("Vracen Iznos");

                    b.HasKey("ID");

                    b.HasIndex("BankaID");

                    b.HasIndex("KorisnikID");

                    b.ToTable("Kredit");
                });

            modelBuilder.Entity("Server.Models.Korisnik", b =>
                {
                    b.HasOne("Server.Models.Banka", "Banka")
                        .WithMany("Korisnici")
                        .HasForeignKey("BankaID");

                    b.Navigation("Banka");
                });

            modelBuilder.Entity("Server.Models.Kredit", b =>
                {
                    b.HasOne("Server.Models.Banka", "Banka")
                        .WithMany("Krediti")
                        .HasForeignKey("BankaID");

                    b.HasOne("Server.Models.Korisnik", "Korisnik")
                        .WithMany("Krediti")
                        .HasForeignKey("KorisnikID");

                    b.Navigation("Banka");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("Server.Models.Banka", b =>
                {
                    b.Navigation("Korisnici");

                    b.Navigation("Krediti");
                });

            modelBuilder.Entity("Server.Models.Korisnik", b =>
                {
                    b.Navigation("Krediti");
                });
#pragma warning restore 612, 618
        }
    }
}
