using ClinicaSorriso.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaSorriso.ConsoleApp.Repositories.EFCore
{
    //Classe que implementa a interface IRepository<Consulta> para manipular os dados no banco
    public class ConsultaRepositoryWithSQLServer : IRepository<Consulta>
    {
        private readonly ClinicaSorrisoContext _context;

        public ConsultaRepositoryWithSQLServer(ClinicaSorrisoContext context)
        {
            _context = context;
        }

        //Método assincrono para deletar uma consulta
        public async Task DeletarAsync(Consulta entity)
        {
            _context.Consultas.Remove(entity);
            await _context.SaveChangesAsync();
        }

        //Método assincrono para listar todas as consultas
        public async Task<List<Consulta>> ListarTodosAsync()
        {
            return await Task.FromResult(_context.Consultas.Include(consulta => consulta.Paciente).ToList());
        }

        //Método assincrono para salvar uma consulta
        public async Task SalvarAsync(Consulta entity)
        {
            await _context.Consultas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }

}
