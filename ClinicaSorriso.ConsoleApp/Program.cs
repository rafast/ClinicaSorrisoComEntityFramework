using ClinicaSorriso.ConsoleApp;
using ClinicaSorriso.ConsoleApp.Controllers;
using ClinicaSorriso.ConsoleApp.Repositories.EFCore;
using ClinicaSorriso.ConsoleApp.Repositories.InMemory;
using ClinicaSorriso.ConsoleApp.Services;

try
{
    ClinicaSorrisoContext context = new();

    PacienteRepositoryWithSQLServer pacienteRepository = new(context);
    ConsultaRepositoryWithSQLServer consultaRepository = new(context);

    PacientesRepositoryInMemory pacientesRepositoryInMemory = new();
    ConsultaRepositoryInMemory consultaRepositoryInMemory = new();

    PacienteService pacienteService = new(pacienteRepository);
    ConsultaService consultaService = new(consultaRepository);

    PacienteController pacienteController = new(pacienteService, consultaService);
    ConsultaController consultaController = new(consultaService, pacienteService);

    //Para popular o banco com alguns pacientes descomentar as duas linhas abaixo
    //SeedRepositories populaRepositorio = new SeedRepositories(pacienteService, consultaService);
    //populaRepositorio.PopularBanco();

    AppController app = new AppController(pacienteController, consultaController);

    app.Run();

}catch(Exception ex)
{
    Console.WriteLine($"Algo deu errado: {ex.Message}");
}
