﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Univoting.Data;

namespace UniVoting.Data.Migrations
{
    [DbContext(typeof(ElectionDbContext))]
    [Migration("20190317231433_AddedAdditionalRankField")]
    partial class AddedAdditionalRankField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UniVoting.Core.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CandidateName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<byte[]>("CandidatePicture");

                    b.Property<int?>("PositionId");

                    b.Property<int?>("RankId");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("RankId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("UniVoting.Core.Commissioner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsChairman");

                    b.Property<bool>("IsPresident");

                    b.Property<string>("Password")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Commissioners");
                });

            modelBuilder.Entity("UniVoting.Core.ElectionConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Colour")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<string>("ElectionName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<string>("EletionSubTitle")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<byte[]>("Logo");

                    b.HasKey("Id");

                    b.ToTable("ElectionConfigurations");
                });

            modelBuilder.Entity("UniVoting.Core.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FacultyName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("UniVoting.Core.PollingStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("PollingStations");
                });

            modelBuilder.Entity("UniVoting.Core.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FacultyId");

                    b.Property<string>("PositionName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("UniVoting.Core.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("VoterRank");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("UniVoting.Core.SkippedVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Positionid");

                    b.Property<int>("VoterId");

                    b.HasKey("Id");

                    b.HasIndex("Positionid");

                    b.HasIndex("VoterId");

                    b.ToTable("SkippedVotes");
                });

            modelBuilder.Entity("UniVoting.Core.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CandidateId");

                    b.Property<int?>("PositionId");

                    b.Property<DateTime?>("Time");

                    b.Property<int?>("VoterId");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("PositionId");

                    b.HasIndex("VoterId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("UniVoting.Core.Voter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FacultyId");

                    b.Property<string>("IndexNumber")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<bool>("VoteInProgress");

                    b.Property<bool>("Voted");

                    b.Property<string>("VoterCode")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<string>("VoterName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Voters");
                });

            modelBuilder.Entity("UniVoting.Core.Candidate", b =>
                {
                    b.HasOne("UniVoting.Core.Position", "Position")
                        .WithMany("Candidates")
                        .HasForeignKey("PositionId");

                    b.HasOne("UniVoting.Core.Rank", "Rank")
                        .WithMany()
                        .HasForeignKey("RankId");
                });

            modelBuilder.Entity("UniVoting.Core.Position", b =>
                {
                    b.HasOne("UniVoting.Core.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniVoting.Core.SkippedVote", b =>
                {
                    b.HasOne("UniVoting.Core.Position", "Position")
                        .WithMany("SkippedVotes")
                        .HasForeignKey("Positionid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniVoting.Core.Voter", "Voter")
                        .WithMany("SkippedVotes")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("UniVoting.Core.Vote", b =>
                {
                    b.HasOne("UniVoting.Core.Candidate", "Candidate")
                        .WithMany("Votes")
                        .HasForeignKey("CandidateId");

                    b.HasOne("UniVoting.Core.Position", "Position")
                        .WithMany("Votes")
                        .HasForeignKey("PositionId");

                    b.HasOne("UniVoting.Core.Voter", "Voter")
                        .WithMany("Votes")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("UniVoting.Core.Voter", b =>
                {
                    b.HasOne("UniVoting.Core.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
