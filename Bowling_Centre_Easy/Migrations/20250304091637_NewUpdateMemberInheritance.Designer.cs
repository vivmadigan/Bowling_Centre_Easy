﻿// <auto-generated />
using System;
using Bowling_Centre_Easy.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bowling_Centre_Easy.Migrations
{
    [DbContext(typeof(BowlingContext))]
    [Migration("20250304091637_NewUpdateMemberInheritance")]
    partial class NewUpdateMemberInheritance
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bowling_Centre_Easy.Abstract.BaseMember", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberID"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<int>("GamesWon")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemberID");

                    b.ToTable("BaseMember");

                    b.HasDiscriminator().HasValue("BaseMember");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.BowlingLane", b =>
                {
                    b.Property<int>("BowlingLaneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BowlingLaneID"));

                    b.Property<bool>("InUse")
                        .HasColumnType("bit");

                    b.Property<int>("LaneNumber")
                        .HasColumnType("int");

                    b.HasKey("BowlingLaneID");

                    b.ToTable("Lanes");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.Frame", b =>
                {
                    b.Property<int>("FrameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FrameId"));

                    b.Property<int>("FrameNumber")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("ScorecardId")
                        .HasColumnType("int");

                    b.HasKey("FrameId");

                    b.HasIndex("ScorecardId");

                    b.ToTable("Frames");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.Match", b =>
                {
                    b.Property<int>("MatchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchID"));

                    b.Property<int>("BowlingLaneID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScorecardId")
                        .HasColumnType("int");

                    b.HasKey("MatchID");

                    b.HasIndex("BowlingLaneID");

                    b.HasIndex("ScorecardId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerID"));

                    b.Property<int>("CurrentScore")
                        .HasColumnType("int");

                    b.Property<int?>("MatchID")
                        .HasColumnType("int");

                    b.Property<int>("MemberInfoMemberID")
                        .HasColumnType("int");

                    b.HasKey("PlayerID");

                    b.HasIndex("MatchID");

                    b.HasIndex("MemberInfoMemberID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.PlayerResult", b =>
                {
                    b.Property<int>("PlayerResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerResultId"));

                    b.Property<int>("FinalScore")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScorecardId")
                        .HasColumnType("int");

                    b.HasKey("PlayerResultId");

                    b.HasIndex("ScorecardId");

                    b.ToTable("PlayerResults");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.Scorecard", b =>
                {
                    b.Property<int>("ScorecardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScorecardId"));

                    b.HasKey("ScorecardId");

                    b.ToTable("Scorecards");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.GuestMember", b =>
                {
                    b.HasBaseType("Bowling_Centre_Easy.Abstract.BaseMember");

                    b.HasDiscriminator().HasValue("GuestMember");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.RegisteredMember", b =>
                {
                    b.HasBaseType("Bowling_Centre_Easy.Abstract.BaseMember");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("RegisteredMember");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.Frame", b =>
                {
                    b.HasOne("Bowling_Centre_Easy.Entities.Scorecard", "Scorecard")
                        .WithMany("Frames")
                        .HasForeignKey("ScorecardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scorecard");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.Match", b =>
                {
                    b.HasOne("Bowling_Centre_Easy.Entities.BowlingLane", "BowlingLane")
                        .WithMany()
                        .HasForeignKey("BowlingLaneID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bowling_Centre_Easy.Entities.Scorecard", "Scorecard")
                        .WithMany()
                        .HasForeignKey("ScorecardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BowlingLane");

                    b.Navigation("Scorecard");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.Player", b =>
                {
                    b.HasOne("Bowling_Centre_Easy.Entities.Match", null)
                        .WithMany("Players")
                        .HasForeignKey("MatchID");

                    b.HasOne("Bowling_Centre_Easy.Abstract.BaseMember", "MemberInfo")
                        .WithMany()
                        .HasForeignKey("MemberInfoMemberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MemberInfo");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.PlayerResult", b =>
                {
                    b.HasOne("Bowling_Centre_Easy.Entities.Scorecard", "Scorecard")
                        .WithMany("Results")
                        .HasForeignKey("ScorecardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scorecard");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.Match", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("Bowling_Centre_Easy.Entities.Scorecard", b =>
                {
                    b.Navigation("Frames");

                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
