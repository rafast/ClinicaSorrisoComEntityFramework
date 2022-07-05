using ClinicaSorriso.ConsoleApp.Models;
using System.Collections.Generic;

namespace ClinicaSorriso.ConsoleApp.Repositories.InMemory
{
    // Implementação da interface do repositório de Pacientes em memória
    public class PacientesRepositoryInMemory : IRepository<Paciente>
    {
        private List<Paciente> _pacientesRepository { get; set; }

        public PacientesRepositoryInMemory()
        {
            _pacientesRepository = new List<Paciente>();
        }

        // Recebe um Paciente e o exclui da base de pacientes
        public async Task DeletarAsync(Paciente entity)
        {
            _pacientesRepository.Remove(entity);
            await Task.CompletedTask;
        }

        // Retorna uma lista com dos os pacientes da base de pacientes
        public async Task<List<Paciente>> ListarTodosAsync()
        {
            return await Task.FromResult(_pacientesRepository);
        }

        // Recebe um paciente e salva na base de pacientes
        public async Task SalvarAsync(Paciente entity)
        {
            _pacientesRepository.Add(entity);
            await Task.CompletedTask;
        }

    }
}
