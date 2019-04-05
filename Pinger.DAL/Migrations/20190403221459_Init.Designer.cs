﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pinger.DAL.EF;

namespace Pinger.DAL.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20190403221459_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("Pinger.DAL.Models.DbSiteOptions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Host");

                    b.Property<int>("PingFrequency");

                    b.HasKey("Id");

                    b.ToTable("SiteOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
