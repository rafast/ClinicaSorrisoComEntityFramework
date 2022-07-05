using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaSorriso.ConsoleApp.Views
{
    // Classe responsável por exibir o Menu Principal
    public static class MenuView
    {

        //Exibe o menu principal da aplicação
        public static void MenuPrincipal()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1-Cadastro de pacientes");
            Console.WriteLine("2-Agenda");
            Console.WriteLine("3-Fim");
        }
    }
}
