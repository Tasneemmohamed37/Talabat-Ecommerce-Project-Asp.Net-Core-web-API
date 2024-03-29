﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Talabat.Reposatory.Data.Context;

#nullable disable

namespace Talabat.Reposatory.Data.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20240219194416_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Talabat.Core.Entities.Product.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Product_BrandId")
                        .HasColumnType("int");

                    b.Property<int>("Product_TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Product_BrandId");

                    b.HasIndex("Product_TypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Talabat.Core.Entities.Product.Product_Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product_Brands");
                });

            modelBuilder.Entity("Talabat.Core.Entities.Product.Product_Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product_Types");
                });

            modelBuilder.Entity("Talabat.Core.Entities.Product.Product", b =>
                {
                    b.HasOne("Talabat.Core.Entities.Product.Product_Brand", "Product_Brand")
                        .WithMany()
                        .HasForeignKey("Product_BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talabat.Core.Entities.Product.Product_Type", "Product_Type")
                        .WithMany()
                        .HasForeignKey("Product_TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product_Brand");

                    b.Navigation("Product_Type");
                });
#pragma warning restore 612, 618
        }
    }
}
