﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExemploEF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200508174102_TabelaAluno_Disciplina")]
    partial class TabelaAluno_Disciplina
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExemploEF.Models.Aluno_Disciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlunoRA")
                        .HasColumnType("int");

                    b.Property<int?>("DisciplinaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlunoRA");

                    b.HasIndex("DisciplinaId");

                    b.ToTable("Aluno_Disciplina");
                });

            modelBuilder.Entity("ExemploEF.Models.Disciplinas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfessorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("ExemploEntityFramework.Models.Aluno", b =>
                {
                    b.Property<int>("RA")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Curso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RA");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("ExemploEntityFramework.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especialidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("ExemploEF.Models.Aluno_Disciplina", b =>
                {
                    b.HasOne("ExemploEntityFramework.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoRA");

                    b.HasOne("ExemploEF.Models.Disciplinas", "Disciplina")
                        .WithMany()
                        .HasForeignKey("DisciplinaId");
                });

            modelBuilder.Entity("ExemploEF.Models.Disciplinas", b =>
                {
                    b.HasOne("ExemploEntityFramework.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId");
                });
#pragma warning restore 612, 618
        }
    }
}