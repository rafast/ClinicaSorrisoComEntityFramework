using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaSorriso.ConsoleApp.Models
{
    // Classe que representa um paciente na aplicação
    public class Paciente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public List<Consulta> ConsultaMarcada { get; set; }

        public Paciente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        // Retorna a idade do paciente
        public int GetIdade()
        {
            var dataHoje = DateTime.Today;
            var idade = dataHoje.Year - DataNascimento.Year;

            if (DataNascimento > dataHoje.AddYears(-idade))
            {
                idade--;
            }
            return idade;
        }

        // Retorna se o paciente possui uma consulta marcada futura
        public bool TemConsultaFutura()
        {
            if (ConsultaMarcada.Count > 0)
            {
                return ConsultaMarcada.First().Data.Date >= DateTime.Now.Date;
            }
            return false;
        }
    }
}
