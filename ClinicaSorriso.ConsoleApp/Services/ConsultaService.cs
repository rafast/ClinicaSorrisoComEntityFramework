using ClinicaSorriso.ConsoleApp.Models;
using ClinicaSorriso.ConsoleApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaSorriso.ConsoleApp.Services
{
    public class ConsultaService
    {
        private IRepository<Consulta> _consultaRepository { get; set; }

        public ConsultaService(IRepository<Consulta> consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        // Recebe uma consulta e salva no repositorio
        public async void CadastrarConsulta(Consulta consulta)
        {
            await _consultaRepository.SalvarAsync(consulta);

        }

        // Recebe uma consulta e exclui do repositorio
        public async void ExcluirConsulta(Consulta consulta)
        {
            await _consultaRepository.DeletarAsync(consulta);
        }

        //Retorna uma lista com todas as consultas do repositorio ordenadas por data
        public async Task<List<Consulta>> ListarConsultas()
        {
            var listaConsultas = await _consultaRepository.ListarTodosAsync();
            return await Task.FromResult(listaConsultas.OrderBy(c => c.Data)
                                                        .ToList());
        }

        //Recebe uma data e retorna uma lista com todas as consultas agendadas para este dia
        public async Task<List<Consulta>> ListarConsultasDoDia(DateTime dataAgendamento)
        {
            var listaConsultasDoDia = await _consultaRepository.ListarTodosAsync();
            return await Task.FromResult(listaConsultasDoDia.Where(c => c.Data.Date == dataAgendamento.Date)
                                                            .ToList());
        }

        //Recebe uma consulta e retorna se há conflito de horário desta consulta com as demais consultas do mesmo dia
        public async Task<bool> TemConflitoDeHorario(Consulta novaConsulta)
        {
            var consultasDoDia = await ListarConsultasDoDia(novaConsulta.Data);
            foreach (var consulta in consultasDoDia)
            {
                if (novaConsulta.TemConflitoDeHorario(consulta))
                {
                    return await Task.FromResult(true);
                }
            }
            return await Task.FromResult(false);
        }

        //Recebe uma data inicial e final e retorna uma lista com todas as consultas agendadas nesse periodo
        public async Task<List<Consulta>> ListarConsultasPorPeriodo(DateTime dtInicio, DateTime dtFim)
        {
            var listaConsultasPorPeriodo = await _consultaRepository.ListarTodosAsync();
            return await Task.FromResult(listaConsultasPorPeriodo.Where(consulta => (consulta.Data.Date >= dtInicio.Date) &
                                                                                    consulta.Data.Date <= dtFim.Date)
                                                                 .ToList());
        }

        //Recebe uma consulta e caso exista no repositorio, retorna ela
        public async Task<Consulta> BuscarConsulta(Consulta consulta)
        {
            var consultaExistente = await _consultaRepository.ListarTodosAsync();
            return await Task.FromResult(consultaExistente.SingleOrDefault(c => c.Paciente.Cpf == consulta.Paciente.Cpf &
                                                                c.Data.Date == consulta.Data.Date &
                                                                c.HoraInicio == consulta.HoraInicio));
        }
    }
}
