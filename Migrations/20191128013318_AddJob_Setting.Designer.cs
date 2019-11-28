﻿// <auto-generated />
using System;
using HandBookApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HandBookApi.Migrations
{
    [DbContext(typeof(HandBookContext))]
    [Migration("20191128013318_AddJob_Setting")]
    partial class AddJob_Setting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("HandBookApi.Models.Base_Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReMark")
                        .HasColumnType("TEXT");

                    b.Property<string>("UsersId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Base_Books");
                });

            modelBuilder.Entity("HandBookApi.Models.Game_Setting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("TEXT");

                    b.Property<int>("Devices")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PassWord")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReMark")
                        .HasColumnType("TEXT");

                    b.Property<int>("TryType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UsersId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Game_Settings");
                });

            modelBuilder.Entity("HandBookApi.Models.Users", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PassWord")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
