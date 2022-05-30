﻿using System;
using ClinicaSorriso.Controllers;
using ClinicaSorriso.Views;
using ClinicaSorriso.Services;
using ClinicaSorriso.Repositories.InMemory;

namespace ClinicaSorriso
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            PacientesRepositoryInMemory pacientesRepositoryInMemory = new PacientesRepositoryInMemory();
            PacienteService pacienteService = new PacienteService(pacientesRepositoryInMemory); 
            PacienteController pacienteController = new PacienteController(pacienteService);

            ConsultaRepositoryInMemory consultaRepositoryInMemory = new ConsultaRepositoryInMemory();
            ConsultaService consultaService = new ConsultaService(consultaRepositoryInMemory);
            ConsultaController consultaController = new ConsultaController(consultaService, pacienteService);

            AppController app = new AppController(pacienteController, consultaController);
            MenuView.MenuPrincipal();
            app.LerOpcaoUsuario();
        }
     
    }
}
