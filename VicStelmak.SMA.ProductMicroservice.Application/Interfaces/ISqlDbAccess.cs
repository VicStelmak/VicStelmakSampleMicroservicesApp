namespace VicStelmak.Sma.ProductMicroservice.Application.Interfaces
{
    public interface ISqlDbAccess
    {
        Task<List<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters);
        Task SaveDataAsync<T>(string storedProcedure, T parameters);
    }
}