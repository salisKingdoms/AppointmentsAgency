﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WS_Appointment.EFCore;

#nullable disable

namespace WS_Appointment.Migrations
{
    [DbContext(typeof(EF_DataContext))]
    partial class EF_DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("WS_Appointment.EFCore.appointments", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("appointmentNo")
                        .HasColumnType("text");

                    b.Property<DateTime>("appointment_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("created_by")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("customer_id")
                        .HasColumnType("bigint");

                    b.Property<string>("serviceType")
                        .IsRequired()
                        .HasColumnType("character varying");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("character varying");

                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.Property<string>("updated_by")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("updated_on")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.ToTable("appointments");
                });

            modelBuilder.Entity("WS_Appointment.EFCore.configuration", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("config_value")
                        .IsRequired()
                        .HasColumnType("character varying");

                    b.Property<string>("created_by")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("updated_by")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("updated_on")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.ToTable("configuration");
                });

            modelBuilder.Entity("WS_Appointment.EFCore.customers", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("address")
                        .HasColumnType("character varying");

                    b.Property<string>("created_by")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("character varying");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("character varying");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("character varying");

                    b.Property<DateTime>("registration_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("updated_by")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("updated_on")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("WS_Appointment.EFCore.public_holidays", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("created_by")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("character varying");

                    b.Property<DateTime>("holiday_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("updated_by")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("updated_on")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.ToTable("public_holidays");
                });
#pragma warning restore 612, 618
        }
    }
}
