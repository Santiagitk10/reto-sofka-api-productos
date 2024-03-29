﻿// <auto-generated />
using System;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DB.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20230105150709_InitMigration")]
    partial class InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DB.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("InInventory")
                        .HasColumnType("int");

                    b.Property<int>("Max")
                        .HasColumnType("int");

                    b.Property<int>("Min")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isEnabled")
                        .HasColumnType("bit");

                    b.HasKey("ProductId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("DB.ProductPurchase", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "PurchaseId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("ProductPurchases");
                });

            modelBuilder.Entity("DB.Purchase", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseId"));

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PurchaseId");

                    b.ToTable("Purchase", (string)null);
                });

            modelBuilder.Entity("DB.ProductPurchase", b =>
                {
                    b.HasOne("DB.Product", "Product")
                        .WithMany("ProductPurchases")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Purchase", "Purchase")
                        .WithMany("ProductPurchases")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("DB.Product", b =>
                {
                    b.Navigation("ProductPurchases");
                });

            modelBuilder.Entity("DB.Purchase", b =>
                {
                    b.Navigation("ProductPurchases");
                });
#pragma warning restore 612, 618
        }
    }
}
