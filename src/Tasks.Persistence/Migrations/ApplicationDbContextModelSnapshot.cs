﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tasks.Persistence;

#nullable disable

namespace Tasks.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tasks.Domain.Person.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Tasks.Domain.Tasks.TodoTask", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssigneeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Tasks.Domain.Tasks.TodoTask", b =>
                {
                    b.HasOne("Tasks.Domain.Person.Person", null)
                        .WithMany()
                        .HasForeignKey("AssigneeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tasks.Domain.Person.Person", null)
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Tasks.Domain.Tasks.TaskDetails.TaskStatistic", "Stats", b1 =>
                        {
                            b1.Property<Guid>("TaskId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime?>("ApprovedDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime?>("CompletionDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("CreatedDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime?>("ReleasedDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime?>("StartedDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime?>("VerifiedDate")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("TaskId");

                            b1.ToTable("Tasks");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });

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

                            b1.Property<string>("Category")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("Description")
                                .HasMaxLength(500)
                                .HasColumnType("character varying(500)");

                            b1.Property<int>("Priority")
                                .HasColumnType("integer");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("TodoTaskId");

                            b1.ToTable("Tasks");

                            b1.WithOwner()
                                .HasForeignKey("TodoTaskId");
                        });

                    b.Navigation("Estimation");

                    b.Navigation("MainInfo")
                        .IsRequired();

                    b.Navigation("Stats");
                });
#pragma warning restore 612, 618
        }
    }
}
