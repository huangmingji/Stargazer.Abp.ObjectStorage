﻿// <auto-generated />
using System;
using Stargazer.Abp.ObjectStorage.EntityFrameworkCore.DbMigrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.ObjectStorage.EntityFrameworkCore.DbMigrations.Migrations
{
    [DbContext(typeof(DbMigrationsDbContext))]
    [Migration("20210912232208_InitialCreateObjectStorage")]
    partial class InitialCreateObjectStorage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.PostgreSql)
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Stargazer.Abp.ObjectStorage.Domain.ObjectData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<string>("ObjectExtension")
                        .HasColumnType("text");

                    b.Property<string>("ObjectHash")
                        .HasColumnType("text");

                    b.Property<string>("ObjectPath")
                        .HasColumnType("text");

                    b.Property<int>("ObjectSize")
                        .HasColumnType("integer");

                    b.Property<string>("ObjectType")
                        .HasColumnType("text");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.ToTable("ObjectData");
                });
#pragma warning restore 612, 618
        }
    }
}
