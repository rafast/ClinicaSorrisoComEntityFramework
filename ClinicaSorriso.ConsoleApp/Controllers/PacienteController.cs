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
    //Classe que recebe e envia os dados PacienteView e interage com o model Paciente e os servicos da aplicação
    public class PacienteController
    {
        private PacienteService _pacienteService { get; set; }
        private ConsultaService _consultaService { get; set; }

        public PacienteController(PacienteService pacienteService, ConsultaService consultaService)
        {
            _pacienteService = pacienteService;
            _consultaService = consultaService;
        }

        // Obtém a opção selecionada pelo usuario no Menu do Paciente e executa a respectiva funcionalidade
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
                        ListarPorCpf();
                        break;
                    case '4':
                        Console.Clear();
                        ListarPorNome();
                        break;
                    case '5':
                        Console.Clear();
                        MenuView.MenuPrincipal();
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        PacienteView.MenuPaciente();
                        break;
                }

            }

        }

        // Executa a lógica de cadastro de um paciente
        public async void Cadastrar()
        {
            try
            {
                await _pacienteService.CadastrarPaciente(PacienteView.Cadastrar());
                PacienteView.CadastroRealizado();
            }
            catch (ArgumentException ex)
            {
                PacienteView.MensagemErro(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro na base de dados: {ex.Message}");
            }
            catch (ApplicationException ex)
            {
                PacienteView.MensagemErro(ex.Message);
            }
        }

        // Executa a lógica de exclusão de um paciente de acordo com o CPF informado
        public async void Excluir()
        {
            try
            {
                var pacienteSalvo = await _pacienteService.ConsultarPacientePorCPF(PacienteView.ConsultarCpf());
                await _pacienteService.ExcluirPaciente(pacienteSalvo);
                PacienteView.PacienteExcluido();
            }
            catch (ApplicationException ex)
            {
                PacienteView.MensagemErro(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro na base de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                PacienteView.MensagemErro(ex.Message);
            }
        }

        // Obtém a lista de pacientes ordenadas por CPF do repositório e envia para ser exibida na PacienteView
        public async void ListarPorCpf()
        {
            try
            {
                var listaPacientesPorCPF = await _pacienteService.ListarPacientesPorCPF();
                PacienteView.ListarPacientes(listaPacientesPorCPF);
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

        // Obtém a lista de pacientes ordenadas por Nome do repositório e envia para ser exibida na PacienteView
        public async void ListarPorNome()
        {
            try
            {
                var listaPacientesPorNome = await _pacienteService.ListarPacientesPorNome();
                PacienteView.ListarPacientes(listaPacientesPorNome);
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
    }
}
