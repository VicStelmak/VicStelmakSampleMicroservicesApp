using Dapper;
using Npgsql;
using System.Data;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Infrastructure.DataAccess
{
    internal class SqlDbAccess : ISqlDbAccess
    {
        string _connectionString;

        public SqlDbAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<T>> LoadDataAsync<T, U>(string storedFunction, U parameters)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

                var rows = await connection.QueryAsync<T>(storedFunction, parameters, commandType: CommandType.Text, commandTimeout: 900);

                return rows.ToList();
            }
        }

        public async Task SaveDataAsync<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
