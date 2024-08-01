﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tasks.Persistence;

#nullable disable

namespace Tasks.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240801093746_ExtendAudutLogWithObjectRelate")]
    partial class ExtendAudutLogWithObjectRelate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tasks.Domain.AuditLog.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("character varying(55)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("ObjectRelateId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("Tasks.Domain.Person.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid")
                        .HasColumnName("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Tasks.Domain.Tasks.TaskDetails.TaskStatistic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ReleasedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("StartedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("VerifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.ToTable("TaskStatistic");
                });

            modelBuilder.Entity("Tasks.Domain.Tasks.TodoTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uuid")
                        .HasColumnName("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Tasks.Domain.Tasks.TaskDetails.TaskStatistic", b =>
                {
                    b.HasOne("Tasks.Domain.Tasks.TodoTask", "Task")
                        .WithOne("Stats")
                        .HasForeignKey("Tasks.Domain.Tasks.TaskDetails.TaskStatistic", "TaskId")
                        .HasPrincipalKey("Tasks.Domain.Tasks.TodoTask", "TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Tasks.Domain.Tasks.TodoTask", b =>
                {
                    b.OwnsOne("Tasks.Domain.Tasks.TaskDetails.TaskEstimation", "Estimation", b1 =>
                        {
                            b1.Property<Guid>("TodoTaskId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("EstimatedEndDateTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("EstimatedStartDateTime")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("TodoTaskId");

                            b1.ToTable("Tasks");

                            b1.WithOwner()
                                .HasForeignKey("TodoTaskId");
                        });

                    b.OwnsOne("Tasks.Domain.Tasks.TaskDetails.TaskMainInfo", "MainInfo", b1 =>
                        {
                            b1.Property<Guid>("TodoTaskId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("AssigneeId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Category")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("Description")
                                .HasMaxLength(500)
                                .HasColumnType("character varying(500)");

                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Priority")
                                .HasColumnType("integer");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("TodoTaskId");

                            b1.HasIndex("AssigneeId");

                            b1.HasIndex("OwnerId");

                            b1.ToTable("Tasks");

                            b1.HasOne("Tasks.Domain.Person.Person", null)
                                .WithMany()
                                .HasForeignKey("AssigneeId")
                                .HasPrincipalKey("PersonId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.HasOne("Tasks.Domain.Person.Person", null)
                                .WithMany()
                                .HasForeignKey("OwnerId")
                                .HasPrincipalKey("PersonId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TodoTaskId");
                        });

                    b.Navigation("Estimation");

                    b.Navigation("MainInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("Tasks.Domain.Tasks.TodoTask", b =>
                {
                    b.Navigation("Stats");
                });
#pragma warning restore 612, 618
        }
    }
}
