﻿// <auto-generated />
using System;
using Fulcrum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fulcrum.Migrations
{
    [DbContext(typeof(FulcrumContext))]
    partial class FulcrumContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fulcrum.Models.Mediafile", b =>
                {
                    b.Property<int>("MediafileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MediafileId"));

                    b.Property<DateTime?>("AddedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Album")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlbumArtist")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Artist")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Bitrate")
                        .HasColumnType("int");

                    b.Property<string>("Codec")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Filename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ImportedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastPlayed")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlayCount")
                        .HasColumnType("int");

                    b.Property<int>("Samplerate")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrackNumber")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("MediafileId");

                    b.ToTable("Mediafiles");
                });

            modelBuilder.Entity("Fulcrum.Models.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionId"));

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Issued")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAgent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SessionId");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Fulcrum.Models.SysConfig", b =>
                {
                    b.Property<int>("SysConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SysConfigId"));

                    b.Property<string>("ArtBasePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FirstRunComplete")
                        .HasColumnType("bit");

                    b.Property<string>("ImportBasePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LibraryBasePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LibraryStructureScheme")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SysConfigId");

                    b.ToTable("SysConfiguration");
                });

            modelBuilder.Entity("Fulcrum.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Registration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fulcrum.Models.Session", b =>
                {
                    b.HasOne("Fulcrum.Models.User", "UserDetails")
                        .WithMany("UserSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Fulcrum.Models.User", b =>
                {
                    b.Navigation("UserSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
