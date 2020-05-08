using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExemploEntityFramework.Models;
using ExemploEF.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Aluno { get; set; }

        public DbSet<Professor> Professor { get; set; }

        public DbSet<Disciplinas> Disciplinas { get; set; }

        public DbSet<Aluno_Disciplina> Aluno_Disciplina { get; set; }
    }
