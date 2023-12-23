using System.Data;
using System.Linq.Expressions;
//using Srikalyanam.Infrastructure.JWT;
//using Srikalyanam.Infrastructure.Logger;
//using Srikalyanam.Infrastructure.Models;
using Dapper;
using Npgsql;
using Serilog;
using LearningAPI.JWT;
//using BCHash = BCrypt.Net.BCrypt;

namespace LearningAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ILogger<T> _seriLogger = null;
        private readonly IConfiguration _configuration;
        private readonly IDataAccess _data;

        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public readonly ITokenRefresher _tokenRefresher;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        public int expiretime = 0;
        public string stringHostName = string.Empty;
        public GenericRepository(ILogger<T> seriLogger, IJwtAuthenticationManager jwtAuthenticationManager, ITokenRefresher tokenRefresher, IRefreshTokenGenerator refreshTokenGenerator, IDataAccess data, IConfiguration config)
        {
            _data = data;
            _configuration = config;
            _seriLogger = seriLogger;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenRefresher = tokenRefresher;
            expiretime = Convert.ToInt16(_configuration["Otpsettings:expiretime"]);

        }

        public List<T1> FetchDataSync<T1, U>(string sqlGetData, U Parameters, string myConnectionString)
        {
            List<T1> DataList = new List<T1>();
            try
            {
                //To do: Make the loggers generic
                _seriLogger.LogInformation("FetchDataAsync called......");
                DataList = _data.LoadDataSync<T1, U>(sqlGetData, Parameters, myConnectionString);

                if (DataList.Count > 0)
                {
                    _seriLogger.LogInformation("SuccessCode : " + Convert.ToInt32(_configuration["ResponsesCodes:SuccessCode"]));
                }
                else
                {
                    _seriLogger.LogWarning("FailureCode : " + Convert.ToInt32(_configuration["ResponsesCodes:FailureCode"]));
                }
                return DataList;
            }
            catch (Exception ex)
            {
                //To do: Make the loggers generic
                _seriLogger.LogError("Exception in FetchDataSync :" + ex.Message);
                //throw ex;
            }
            return DataList;
        }

        public async Task<List<T1>> FetchDataAsync<T1, U>(string sqlGetData, U Parameters, string myConnectionString)
        {
            List<T1> DataList = new List<T1>();
            try
            {
                //To do: Make the loggers generic
                _seriLogger.LogInformation("FetchDataAsync called......");
                DataList = await _data.LoadData<T1, U>(sqlGetData, Parameters, myConnectionString);

                if (DataList.Count > 0)
                {
                    _seriLogger.LogInformation("SuccessCode : " + Convert.ToInt32(_configuration["ResponsesCodes:SuccessCode"]));
                }
                else
                {
                    _seriLogger.LogWarning("FailureCode : " + Convert.ToInt32(_configuration["ResponsesCodes:FailureCode"]));
                }
                return DataList;
            }
            catch (Exception ex)
            {
                //To do: Make the loggers generic
                _seriLogger.LogError("Exception in FetchAllocationApplicationCountDataAsync :" + ex.Message);
                //throw ex;
            }
            return DataList;
        }
        public async Task<IEnumerable<T>> FetchDataAsyncc<T, U>(string sql, U parameters, string connectionString, string commandType)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }
        public async Task<dynamic> FetchDataAsync<U>(string sqlGetData, U Parameters, string myConnectionString)
        {
            try
            {
                _seriLogger.LogInformation("FetchDataAsync called......");
                return await _data.LoadData<U>(sqlGetData, Parameters, myConnectionString);
            }
            catch (Exception ex)
            {
                _seriLogger.LogError("Exception in FetchAllocationApplicationCountDataAsync :" + ex.Message);
                //throw ex;
            }
            return 0;
        }

        public async Task<List<T>> FetchMultipleAsync<T, T1, T2, U>(string sqlGetData, U Parameters, string myConnectionString)
        {
            List<T> DataList = new List<T>();
            try
            {
                //To do: Make the loggers generic
                _seriLogger.LogInformation("FetchMultipleAsync called......");
                DataList = await _data.LoadMultipleData<T, T1, T2, U>(sqlGetData, Parameters, myConnectionString);

                if (DataList.Count > 0)
                {
                    _seriLogger.LogInformation("SuccessCode : " + Convert.ToInt32(_configuration["ResponsesCodes:SuccessCode"]));
                }
                else
                {
                    _seriLogger.LogWarning("FailureCode : " + Convert.ToInt32(_configuration["ResponsesCodes:FailureCode"]));
                }
                return DataList;
            }
            catch (Exception ex)
            {
                //To do: Make the loggers generic
                _seriLogger.LogError("Exception in FetchAllocationApplicationCountDataAsync :" + ex.Message);
            }
            return DataList;
        }

        public async Task<List<T>> FetchMultipleAsync<T, T1, T2, T3, U>(string sqlGetData, U Parameters, string myConnectionString)
        {
            List<T> DataList = new List<T>();
            try
            {
                //To do: Make the loggers generic
                _seriLogger.LogInformation("FetchMultipleAsync called......");
                DataList = await _data.LoadMultipleData<T, T1, T2, T3, U>(sqlGetData, Parameters, myConnectionString);

                if (DataList.Count > 0)
                {
                    _seriLogger.LogInformation("SuccessCode : " + Convert.ToInt32(_configuration["ResponsesCodes:SuccessCode"]));
                }
                else
                {
                    _seriLogger.LogWarning("FailureCode : " + Convert.ToInt32(_configuration["ResponsesCodes:FailureCode"]));
                }
                return DataList;
            }
            catch (Exception ex)
            {
                //To do: Make the loggers generic
                _seriLogger.LogError("Exception in FetchAllocationApplicationCountDataAsync :" + ex.Message);
            }
            return DataList;
        }

        public async Task<List<T>> FetchMultipleAsync<T, T1, T2, T3, T4, U>(string sqlGetData, U Parameters, string myConnectionString)
        {
            List<T> DataList = new List<T>();
            try
            {
                //To do: Make the loggers generic
                _seriLogger.LogInformation("FetchMultipleAsync called......");
                DataList = await _data.LoadMultipleData<T, T1, T2, T3, T4, U>(sqlGetData, Parameters, myConnectionString);

                if (DataList.Count > 0)
                {
                    _seriLogger.LogInformation("SuccessCode : " + Convert.ToInt32(_configuration["ResponsesCodes:SuccessCode"]));
                }
                else
                {
                    _seriLogger.LogWarning("FailureCode : " + Convert.ToInt32(_configuration["ResponsesCodes:FailureCode"]));
                }
                return DataList;
            }
            catch (Exception ex)
            {
                //To do: Make the loggers generic
                _seriLogger.LogError("Exception in FetchAllocationApplicationCountDataAsync :" + ex.Message);
                throw ex;
            }
            return DataList;
        }
        public async Task<List<T1>> FetchDataAsync<T1>(string sqlGetData, string myConnectionString, string methodName)
        {
            List<T1> DataList = new List<T1>();
            try
            {
                //To do: Make the loggers generic
                Log.Information("FetchDataAsync called......");
                DataList = await _data.LoadMasterData<T1>(sqlGetData, myConnectionString, methodName);
                Log.Information("FetchDataAsync end......");
                if (DataList.Count > 0)
                {
                    _seriLogger.LogInformation("SuccessCode : " + Convert.ToInt32(_configuration["ResponsesCodes:SuccessCode"]));
                }
                else
                {
                    _seriLogger.LogWarning("FailureCode : " + Convert.ToInt32(_configuration["ResponsesCodes:FailureCode"]));
                }
                return DataList;
            }
            catch (Exception ex)
            {
                //To do: Make the loggers generic
                _seriLogger.LogError("Exception in FetchAllocationApplicationCountDataAsync :" + ex.Message);
                throw ex;
            }
            return DataList;
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int SaveData<U>(string sqlGetData, U Parameters, string myConnectionString)
        {
            try
            {
                try
                {
                    return _data.SaveData<U>(sqlGetData, Parameters, myConnectionString);

                }
                catch (Exception ex)
                {
                    _seriLogger.LogError("Exception in SaveData of Generic Repository :" + ex.Message);
                }
                return 0;

            }
            catch (Exception ex)
            {
                _seriLogger.LogError("Exception in method SaveData of Generic Repository: {id} ", ex.Message);
                throw;
            }
        }

        public object SaveDataScalar<T, U>(U Parameters, string connectionstring, string spname)
        {
            _seriLogger.LogInformation("SaveDataScalar called.......");
            object obj = new object();
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (IDbTransaction _dbTransaction = connection.BeginTransaction())
                {
                    try
                    {
                        obj = connection.ExecuteScalar(spname, Parameters, commandType: CommandType.StoredProcedure);
                        _dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _dbTransaction.Rollback();
                        _seriLogger.LogError("Exception while running SaveDataScalar: : {id} ", ex.Message);
                        throw new Exception(ex.Message);
                        //_globalLogger.LogError("Event ID - {0} - Exception - {1}: ", GlobalEngine.HISEventID.GlobalEvent.GetLoggerEventId.Id, ex.Message + ex.StackTrace);
                    }
                    return obj;
                }
            }
        }
    }
}
