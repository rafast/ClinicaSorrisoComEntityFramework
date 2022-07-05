using ClinicaSorriso.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaSorriso.ConsoleApp.Repositories.EFCore
{
    //Classe que herda de DbContext e configura o contexto da aplicação para utilização o Entity Framework Core
    public class ClinicaSorrisoContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        //Configurando o relacionamento de 1 para n entre um Paciente e Consulta
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Consulta>()
                .HasOne(consulta => consulta.Paciente)
                .WithMany(paciente => paciente.ConsultaMarcada)
                .HasForeignKey(consulta => consulta.PacienteId);
        }

        //Configurando o contexto para utilizar o SQL Server
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ClinicaSorrisoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}
