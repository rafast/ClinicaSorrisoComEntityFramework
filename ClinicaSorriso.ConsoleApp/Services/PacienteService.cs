using ClinicaSorriso.ConsoleApp.Models;
using ClinicaSorriso.ConsoleApp.Repositories;
using ClinicaSorriso.ConsoleApp.Repositories.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaSorriso.ConsoleApp.Services
{
    // Classe que implementa a interface de serviços do Paciente
    public class PacienteService
    {
        private IRepository<Paciente> _pacientesRepository { get; set; }

        public PacienteService(IRepository<Paciente> pacientesRepository)
        {
            _pacientesRepository = pacientesRepository;
        }

        // Recebe um paciente e verifica de acordo com as regras de negócio se o cadastro pode ou não ser realizado
        public async Task CadastrarPaciente(Paciente paciente)
        {
            var existePaciente = await ConsultarPacientePorCPF(paciente.Cpf);

            if (existePaciente != null)
            {
                throw new ArgumentException("CPF já cadastrado.");
            }

            if (paciente.GetIdade() < 13)
            {
                throw new ApplicationException($"paciente só tem {paciente.GetIdade()} anos.");
            }
            await _pacientesRepository.SalvarAsync(paciente);
        }

        // Recebe um CPF e retorna um paciente, caso caso o mesmo esteja cadastrado na base de pacientes
        public async Task<Paciente> ConsultarPacientePorCPF(string cpf)
        {
            var pacienteExistente = await _pacientesRepository.ListarTodosAsync();
            return await Task.FromResult(pacienteExistente.SingleOrDefault(p => p.Cpf == cpf));
        }

        // Recebe um paciente e verifica de acordo com as regras de negócio se o paciente pode ou não ser excluído
        public async Task ExcluirPaciente(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new ApplicationException("paciente não cadastrado.");
            }

            if (paciente.TemConsultaFutura())
            {
                throw new ApplicationException($"paciente está agendado para {paciente.ConsultaMarcada.First().Data:dd/MM/yyyy} as " +
                                    $"{paciente.ConsultaMarcada.First().GetHorario(paciente.ConsultaMarcada.First().HoraInicio)}h.");
            }
            await _pacientesRepository.DeletarAsync(paciente);            
        }

        // Retorna uma lista da base de pacientes ordenada por CPF
        public async Task<List<Paciente>> ListarPacientesPorCPF()
        {
            var listaPacientesPorCpf = await _pacientesRepository.ListarTodosAsync();
            return await Task.FromResult(listaPacientesPorCpf.OrderBy(p => p.Cpf).ToList());
        }

        // Retorna uma lista da base de pacientes ordenada por Nome
        public async Task<List<Paciente>> ListarPacientesPorNome()
        {
            var listaPacientesPorNome = await _pacientesRepository.ListarTodosAsync();
            return await Task.FromResult(listaPacientesPorNome.OrderBy(p => p.Nome).ToList());
        }
    }
}
