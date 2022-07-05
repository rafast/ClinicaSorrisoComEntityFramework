using ClinicaSorriso.ConsoleApp.Models;
using ClinicaSorriso.ConsoleApp.Services;
using ClinicaSorriso.ConsoleApp.Views;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaSorriso.ConsoleApp.Controllers
{
    //Classe que recebe e envia os dados ConsultaView e interage com o model Consulta e Paciente, e os servicos da aplicação
    public class ConsultaController
    {
        private PacienteService _pacienteService { get; set; }
        private ConsultaService _consultaService { get; set; }

        public ConsultaController(ConsultaService consultaService, PacienteService pacienteService)
        {
            _consultaService = consultaService;
            _pacienteService = pacienteService;
        }

        // Obtém a opção selecionada pelo usuario no Menu da Agenda e executa a respectiva funcionalidade
        public void LeituraOpcao()
        {
            bool exit = false;

            while (!exit)
            {
                var opcao = Console.ReadKey();
                switch (opcao.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Cadastrar();
                        break;
                    case '2':
                        Console.Clear();
                        Excluir();
                        break;
                    case '3':
                        Console.Clear();
                        ListarAgenda();
                        break;
                    case '4':
                        Console.Clear();
                        MenuView.MenuPrincipal();
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        ConsultaView.MenuAgenda();
                        break;
                }

            }
        }

        // Executa a lógica de agendamento de uma consulta
        public async void Cadastrar()
        {
            try
            {
                var pacienteExistente = await _pacienteService.ConsultarPacientePorCPF(PacienteView.ConsultarCpf());
                if (pacienteExistente is null)
                {
                    PacienteView.MensagemErro("paciente não cadastrado.");
                    return;
                }

                if (pacienteExistente.TemConsultaFutura())
                {
                    string mensagemErro = $" o paciente já possui consulta marcada para " +
                        $"{pacienteExistente.ConsultaMarcada.First().Data:dd/MM/yyyy} as " +
                        $"{pacienteExistente.ConsultaMarcada.First().HoraInicio}h";

                    PacienteView.MensagemErro(mensagemErro);
                    return;
                }

                var dadosConsulta = ConsultaView.ObterDadosConsulta();

                var novaConsulta = new Consulta(pacienteExistente,
                                                DateTime.Parse(dadosConsulta.DataConsulta),
                                                dadosConsulta.HoraInicio,
                                                dadosConsulta.HoraFim);

                var temConflitoDeHorario = await _consultaService.TemConflitoDeHorario(novaConsulta);
                if (temConflitoDeHorario)
                {
                    PacienteView.MensagemErro("já existe uma consulta agendada nesta data/hora.");
                    return;
                }
                else
                {
                    _consultaService.CadastrarConsulta(novaConsulta);                
                    ConsultaView.AgendamentoRealizado();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro na base de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Algo deu errado: {ex.Message}");
            }

        }

        // Executa a lógica de cancelamento de uma consulta
        public async void Excluir()
        {
            try
            {
                var pacienteExistente = await _pacienteService.ConsultarPacientePorCPF(PacienteView.ConsultarCpf());
                if (pacienteExistente is null)
                {
                    PacienteView.MensagemErro("paciente não cadastrado.");
                    return;
                }

                var dadosConsulta = ConsultaView.ObterDadosConsulta();

                var consultaExcluir = new Consulta(pacienteExistente,
                                                DateTime.Parse(dadosConsulta.DataConsulta),
                                                dadosConsulta.HoraInicio,
                                                dadosConsulta.HoraFim);

                var consultaExistente = await _consultaService.BuscarConsulta(consultaExcluir);

                if (consultaExistente is null)
                {
                    ConsultaView.MensagemErro("agendamento não encontrado.");
                    return;
                }

                _consultaService.ExcluirConsulta(consultaExistente);
                ConsultaView.ConsultaExcluida();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro na base de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Algo deu errado: {ex.Message}");
            }
        }

        //Obtem o tipo de listagem escolhida e passa para a View a lista correspondente
        public async void ListarAgenda()
        {
            var opcaoListagem = ConsultaView.ObterOpcaoListagem();
            Console.Clear();
            if (opcaoListagem == 'T' || opcaoListagem == 't')
            {
                try
                {
                    var listaAgendaTotal = await _consultaService.ListarConsultas();
                    ConsultaView.ListarAgenda(listaAgendaTotal);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Erro na base de dados: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Algo deu errado: {ex.Message}");
                }
            }
            if (opcaoListagem == 'P' || opcaoListagem == 'p')
            {
                var datasPeriodo = ConsultaView.ObterPeriodoListagem();
                try
                {
                    var listaAgendaPorPeriodo = await _consultaService.ListarConsultasPorPeriodo(datasPeriodo[0], datasPeriodo[1]);
                    ConsultaView.ListarAgenda(listaAgendaPorPeriodo);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Erro na base de dados: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Algo deu errado: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine();
            }
        }

    }
}
