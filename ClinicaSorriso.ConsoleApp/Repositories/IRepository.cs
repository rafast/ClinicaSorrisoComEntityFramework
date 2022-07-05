namespace ClinicaSorriso.ConsoleApp.Repositories
{
    // Interface do repositório de dados
    public interface IRepository<T>
    {
        Task<List<T>> ListarTodosAsync();
        Task SalvarAsync(T entity);
        Task DeletarAsync(T entity);
    }
}
