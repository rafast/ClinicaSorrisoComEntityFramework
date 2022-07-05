using ClinicaSorriso.ConsoleApp.Models;
using System.Collections.Generic;

namespace ClinicaSorriso.ConsoleApp.Repositories.InMemory
{
    // Implementação da interface do repositório de Consultas em memória
    public class ConsultaRepositoryInMemory : IRepository<Consulta>
    {
        private List<Consulta> _consultaRepository { get; set; }

        public ConsultaRepositoryInMemory()
        {
            _consultaRepository = new List<Consulta>();
        }

        // Recebe uma Consulta e salva na base de consultas
        public async Task SalvarAsync(Consulta entity)
        {
            _consultaRepository.Add(entity);
            await Task.CompletedTask;
        }

        // Recebe uma Consulta e exclui da base de consultas
        public async Task DeletarAsync(Consulta entity)
        {
            _consultaRepository.Remove(entity);
            await Task.CompletedTask;
        }

        // Retorna uma lista com todas as consultas da base de consultas
        public async Task<List<Consulta>> ListarTodosAsync()
        {
            return await Task.FromResult(_consultaRepository);
        }

    }
}
