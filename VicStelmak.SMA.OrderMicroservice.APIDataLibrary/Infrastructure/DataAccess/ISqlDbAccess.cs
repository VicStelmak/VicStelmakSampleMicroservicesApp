
namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Infrastructure.DataAccess
{
    internal interface ISqlDbAccess
    {
        Task<List<T>> LoadDataAsync<T, U>(string storedFunction, U parameters);
        Task SaveDataAsync<T>(string storedProcedure, T parameters);
    }
}