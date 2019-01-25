﻿// <auto-generated />
using System;
using BeltExam.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeltExam.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190125161908_ActualMainMigration")]
    partial class ActualMainMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BeltExam.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityTitle")
                        .IsRequired();

                    b.Property<int>("CoordinatorId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("Duration");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("TimeMeasure")
                        .IsRequired();

                    b.HasKey("ActivityId");

                    b.HasIndex("CoordinatorId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("BeltExam.Models.Participation", b =>
                {
                    b.Property<int>("ParticipationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityId");

                    b.Property<int>("UserId");

                    b.HasKey("ParticipationId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Participations");
                });

            modelBuilder.Entity("BeltExam.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BeltExam.Models.Activity", b =>
                {
                    b.HasOne("BeltExam.Models.User", "Coordinator")
                        .WithMany("OwnedActivities")
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeltExam.Models.Participation", b =>
                {
                    b.HasOne("BeltExam.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeltExam.Models.User", "User")
                        .WithMany("Participations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
