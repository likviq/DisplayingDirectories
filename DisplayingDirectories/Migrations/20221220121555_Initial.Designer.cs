﻿// <auto-generated />
using System;
using DisplayingDirectories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DisplayingDirectories.Migrations
{
    [DbContext(typeof(DirectoryDBContext))]
    [Migration("20221220121555_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DisplayingDirectories.Models.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FolderType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Folders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FolderType = "Directory",
                            Name = "Creating Digital Images"
                        },
                        new
                        {
                            Id = 2,
                            FolderType = "Directory",
                            Name = "Resources",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 3,
                            FolderType = "Directory",
                            Name = "Evidence",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 4,
                            FolderType = "Directory",
                            Name = "Graphic Products",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 5,
                            FolderType = "Directory",
                            Name = "Primary Sources",
                            ParentId = 2
                        },
                        new
                        {
                            Id = 6,
                            FolderType = "Directory",
                            Name = "Secondary Sources",
                            ParentId = 2
                        },
                        new
                        {
                            Id = 7,
                            FolderType = "Directory",
                            Name = "Process",
                            ParentId = 4
                        },
                        new
                        {
                            Id = 8,
                            FolderType = "Directory",
                            Name = "Final Product",
                            ParentId = 4
                        });
                });

            modelBuilder.Entity("DisplayingDirectories.Models.Folder", b =>
                {
                    b.HasOne("DisplayingDirectories.Models.Folder", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });
#pragma warning restore 612, 618
        }
    }
}