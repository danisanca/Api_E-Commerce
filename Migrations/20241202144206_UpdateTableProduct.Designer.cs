﻿// <auto-generated />
using System;
using ApiEstoque.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiEstoque.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20241202144206_UpdateTableProduct")]
    partial class UpdateTableProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiEstoque.Models.CategoriesModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("shopId")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("shopId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ApiEstoque.Models.EvidenceModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("drescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("productId");

                    b.HasIndex("userId");

                    b.ToTable("Evidence");
                });

            modelBuilder.Entity("ApiEstoque.Models.HistoryMovimentModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("action")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<float>("amount")
                        .HasMaxLength(45)
                        .HasColumnType("real");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("productId")
                        .IsUnicode(true)
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("productId");

                    b.HasIndex("userId");

                    b.ToTable("HistoryMoviment");
                });

            modelBuilder.Entity("ApiEstoque.Models.HistoryPurchaseModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<float>("amount")
                        .HasMaxLength(45)
                        .HasColumnType("real");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("productId")
                        .IsUnicode(true)
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("productId");

                    b.HasIndex("userId");

                    b.ToTable("HistoryPurchase");
                });

            modelBuilder.Entity("ApiEstoque.Models.ImageModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("shopId")
                        .HasColumnType("int");

                    b.Property<float>("size")
                        .HasColumnType("real");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("id");

                    b.HasIndex("shopId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("ApiEstoque.Models.ProductModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("categoriesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("imageId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(45)");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.Property<int>("shopId")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("categoriesId");

                    b.HasIndex("imageId");

                    b.HasIndex("shopId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ApiEstoque.Models.ScoreProductModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<float>("amountStars")
                        .HasMaxLength(45)
                        .HasColumnType("real");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("productId");

                    b.HasIndex("userId");

                    b.ToTable("ScoreProduct");
                });

            modelBuilder.Entity("ApiEstoque.Models.ShopModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("ApiEstoque.Models.StockModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<float>("amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("productId")
                        .IsUnicode(true)
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("productId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("ApiEstoque.Models.UserModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("typeAccout")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(45)");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ApiEstoque.Models.CategoriesModel", b =>
                {
                    b.HasOne("ApiEstoque.Models.ShopModel", "Shop")
                        .WithMany("categories")
                        .HasForeignKey("shopId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("ApiEstoque.Models.EvidenceModel", b =>
                {
                    b.HasOne("ApiEstoque.Models.ProductModel", "product")
                        .WithMany("evidences")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiEstoque.Models.UserModel", "user")
                        .WithMany("evidences")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ApiEstoque.Models.HistoryMovimentModel", b =>
                {
                    b.HasOne("ApiEstoque.Models.ProductModel", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiEstoque.Models.UserModel", "user")
                        .WithMany("historyMoviments")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ApiEstoque.Models.HistoryPurchaseModel", b =>
                {
                    b.HasOne("ApiEstoque.Models.ProductModel", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiEstoque.Models.UserModel", "user")
                        .WithMany("historyPurchases")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ApiEstoque.Models.ImageModel", b =>
                {
                    b.HasOne("ApiEstoque.Models.ShopModel", "Shop")
                        .WithMany()
                        .HasForeignKey("shopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("ApiEstoque.Models.ProductModel", b =>
                {
                    b.HasOne("ApiEstoque.Models.CategoriesModel", "categories")
                        .WithMany()
                        .HasForeignKey("categoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiEstoque.Models.ImageModel", "image")
                        .WithMany()
                        .HasForeignKey("imageId");

                    b.HasOne("ApiEstoque.Models.ShopModel", "shop")
                        .WithMany("products")
                        .HasForeignKey("shopId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("categories");

                    b.Navigation("image");

                    b.Navigation("shop");
                });

            modelBuilder.Entity("ApiEstoque.Models.ScoreProductModel", b =>
                {
                    b.HasOne("ApiEstoque.Models.ProductModel", "product")
                        .WithMany("scoreProducts")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiEstoque.Models.UserModel", "user")
                        .WithMany("scoreProducts")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ApiEstoque.Models.ShopModel", b =>
                {
                    b.HasOne("ApiEstoque.Models.UserModel", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("ApiEstoque.Models.StockModel", b =>
                {
                    b.HasOne("ApiEstoque.Models.ProductModel", "product")
                        .WithMany("stocks")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("ApiEstoque.Models.ProductModel", b =>
                {
                    b.Navigation("evidences");

                    b.Navigation("scoreProducts");

                    b.Navigation("stocks");
                });

            modelBuilder.Entity("ApiEstoque.Models.ShopModel", b =>
                {
                    b.Navigation("categories");

                    b.Navigation("products");
                });

            modelBuilder.Entity("ApiEstoque.Models.UserModel", b =>
                {
                    b.Navigation("evidences");

                    b.Navigation("historyMoviments");

                    b.Navigation("historyPurchases");

                    b.Navigation("scoreProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
