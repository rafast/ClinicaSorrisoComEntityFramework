using ClinicaSorriso.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaSorriso.ConsoleApp.Repositories.EFCore
{
    //Classe que implementa a interface IRepository<Paciente> para manipular os dados no banco
    public class PacienteRepositoryWithSQLServer : IRepository<Paciente>
    {
        private readonly ClinicaSorrisoContext _context;

        public PacienteRepositoryWithSQLServer(ClinicaSorrisoContext context)
        {
            _context = context;
        }
        
        //Método assincrono para deletar um paciente
        public async Task DeletarAsync(Paciente entity)
        {
            _context.Pacientes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        //Método assincrono para listar todos os pacientes
        public async Task<List<Paciente>> ListarTodosAsync()
        {
            return await Task.FromResult(_context.Pacientes.Include(paciente => paciente.ConsultaMarcada
                                                           .OrderByDescending(consulta => consulta.Data))
                                                           .ToList());
        }

        //Método assincrono para salvar um paciente
        public async Task SalvarAsync(Paciente entity)
        {
            await _context.Pacientes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
