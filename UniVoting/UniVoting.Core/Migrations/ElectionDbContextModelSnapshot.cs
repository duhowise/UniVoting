﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniVoting.Core;

namespace UniVoting.Core.Migrations
{
    [DbContext(typeof(ElectionDbContext))]
    partial class ElectionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UniVoting.Model.Candidate", b =>
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

            modelBuilder.Entity("UniVoting.Model.Comissioner", b =>
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

                    b.ToTable("Comissioners");
                });

            modelBuilder.Entity("UniVoting.Model.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Faculty")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<string>("PositionName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("UniVoting.Model.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("UniVoting.Model.Setting", b =>
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

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("UniVoting.Model.SkippedVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Positionid");

                    b.Property<int>("VoterId");

                    b.HasKey("Id");

                    b.HasIndex("Positionid");

                    b.HasIndex("VoterId");

                    b.ToTable("SkippedVoteses");
                });

            modelBuilder.Entity("UniVoting.Model.Vote", b =>
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

            modelBuilder.Entity("UniVoting.Model.Voter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Faculty")
                        .HasMaxLength(256)
                        .IsUnicode(false);

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

                    b.ToTable("Voters");
                });

            modelBuilder.Entity("UniVoting.Model.Candidate", b =>
                {
                    b.HasOne("UniVoting.Model.Position", "Position")
                        .WithMany("Candidates")
                        .HasForeignKey("PositionId");

                    b.HasOne("UniVoting.Model.Rank", "Rank")
                        .WithMany("Candidates")
                        .HasForeignKey("RankId");
                });

            modelBuilder.Entity("UniVoting.Model.SkippedVote", b =>
                {
                    b.HasOne("UniVoting.Model.Position", "Position")
                        .WithMany("SkippedVotes")
                        .HasForeignKey("Positionid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniVoting.Model.Voter", "Voter")
                        .WithMany()
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniVoting.Model.Vote", b =>
                {
                    b.HasOne("UniVoting.Model.Candidate", "Candidate")
                        .WithMany("Votes")
                        .HasForeignKey("CandidateId");

                    b.HasOne("UniVoting.Model.Position", "Position")
                        .WithMany("Votes")
                        .HasForeignKey("PositionId");

                    b.HasOne("UniVoting.Model.Voter", "Voter")
                        .WithMany("Votes")
                        .HasForeignKey("VoterId");
                });
#pragma warning restore 612, 618
        }
    }
}