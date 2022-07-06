# ClinicaSorriso 2.0

A nova versão possui persistência dos dados utilizando o Entity Framework Core juntamente com o SQL Server.

Os requisitos da versão 2.0 da aplicação encontra-se no arquivo Back-end C# - Desafio #3.pdf

Para executar a aplicação é necessário criar uma instância do banco de dados SQL Server e inserir a sua ConnectionString como parâmetro do método OnConfiguring em 
ClinicaSorriso.ConsoleApp.Repositories.EFCore.ClinicaSorrisoContext.cs.

Ex: 
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTIONSTRING);
        }
