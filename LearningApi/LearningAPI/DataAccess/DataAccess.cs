using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using LearningAPI.Repositories;
using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using Npgsql;
using Serilog;
using LearningAPI.DataAccess;
//using System.Data.SqlClient;

namespace LearningAPI.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private Serilog.ILogger _seriLogger = null;
        //GlobalLogger _globalLogger;
        public DataAccess()
        {
            _seriLogger = Serilog.Log.Logger;
        }

        public DataAccess(Serilog.ILogger seriLogger)
        {
            _seriLogger = seriLogger;
        }

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        public async Task<dynamic> LoadData<U>(string sql, U Parameters, string connectionstring)
        {
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    return await connection.QueryAsync(sql, Parameters);
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadData :: " + ex.Message);
                throw;
            }

        }

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        public List<T> LoadDataSync<T, U>(string sql, U Parameters, string connectionstring)
        {
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    var rows = connection.Query<T>(sql, Parameters);
                    return rows.ToList();
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadData :: " + ex.Message);
                throw new Exception(ex.Message);
            }

        }

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        public async Task<List<T>> LoadData<T, U>(string sql, U Parameters, string connectionstring)
        {
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    var rows = await connection.QueryAsync<T>(sql, Parameters);
                    return rows.ToList();
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadData :: " + ex.Message);
                throw;
            }

        }

        public async Task<List<T>> LoadMultipleData<T, T1, T2, U>(string sql, U Parameters, string connectionstring)
        {
            var dataList = new List<T>();
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    using (var reader = connection.QueryMultiple(sql, Parameters, null, null, null))
                    {
                        var Set1 = reader.Read<T1>().ToList();
                        var Set2 = reader.Read<T2>().ToList();

                        dataList.AddRange((IEnumerable<T>)Set1);
                        dataList.AddRange((IEnumerable<T>)Set2);
                    }

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadMultipleData :: " + ex.Message);
                throw;
            }
        }

        public async Task<List<T>> LoadMultipleData<T, T1, T2, T3, U>(string sql, U Parameters, string connectionstring)
        {
            var dataList = new List<T>();
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    using (var reader = connection.QueryMultiple(sql, Parameters, null, null, null))
                    {
                        var Set1 = reader.Read<T1>().ToList();
                        var Set2 = reader.Read<T2>().ToList();
                        var Set3 = reader.Read<T3>().ToList();

                        dataList.AddRange((IEnumerable<T>)Set1);
                        dataList.AddRange((IEnumerable<T>)Set2);
                        dataList.AddRange((IEnumerable<T>)Set3);
                    }

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadMultipleData :: " + ex.Message);
                throw;
            }
        }


        public async Task<List<T>> LoadMultipleData<T, T1, T2, T3, T4, U>(string sql, U Parameters, string connectionstring)
        {
            var dataList = new List<T>();
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    using (var reader = connection.QueryMultiple(sql, Parameters, null, null, null))
                    {
                        var Set1 = reader.Read<T1>().ToList();
                        var Set2 = reader.Read<T2>().ToList();
                        var Set3 = reader.Read<T3>().ToList();
                        var Set4 = reader.Read<T4>().ToList();

                        dataList.AddRange((IEnumerable<T>)Set1);
                        dataList.AddRange((IEnumerable<T>)Set2);
                        dataList.AddRange((IEnumerable<T>)Set3);
                        dataList.AddRange((IEnumerable<T>)Set4);

                    }

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadMultipleData :: " + ex.Message);
                throw;
            }
        }
        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        public async Task<List<T>> LoadMasterData<T>(string sql, string connectionstring, string methodName)
        {
            try
            {
                Log.Information("LoadMasterData called......");
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    Log.Information("connection sring :  " + connectionstring);
                    Log.Information("connection establised......");
                    Log.Information("sql query is......" + sql);
                    var rows = await connection.QueryAsync<T>(sql);
                    return rows.ToList();
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess of LoadMasterData from Controller Method Name {methodName}: {exMessage}", methodName, ex.Message);
                throw;
            }
        }

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        public async Task<T> LoadTypeData<T>(string sql, string connectionstring)
        {

            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    var rows = await connection.QueryAsync<T>(sql);
                    return (T)rows;
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadTypeData :: " + ex.Message);
                throw;
            }
        }

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        public async Task<List<T>> LoadSingleData<T, U>(string sql, U Parameters, string connectionstring)
        {
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    var rows = await connection.QueryAsync<T>(sql, Parameters);
                    return rows.ToList();
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadSingleData :: " + ex.Message);
                throw;
            }
        }

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        public T LoadSingleRecord<T, U>(string sql, U Parameters, string connectionstring)
        {
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    var rows = connection.QueryFirst<T>(sql, Parameters);
                    return rows;
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadSingleRecord :: " + ex.Message);
                throw;
            }
        }

        [Description("Method to retrieve data using Stored Procedure"), Category("DataAccess")]
        public async Task<List<T>> LoadData<T, U>(string sqlSP, U Parameters, string connectionstring, string spname)
        {
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    var rows = await connection.QueryAsync<T>(sqlSP, Parameters, commandType: CommandType.StoredProcedure);
                    return rows.ToList();
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - LoadData :: " + ex.Message);
                throw;
            }
        }



        public int SaveData<T>(string sql, T Parameters, string connectionstring)
        {
            int rows = 0;
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                try
                {
                    rows = connection.Execute(sql, Parameters);
                }
                catch (Exception ex)
                {
                    _seriLogger.Error("Exception in DataAccess - SaveData :: " + ex.Message);
                    throw;
                }
            }
            return rows;
        }


        public Task SaveDataReader<T>(string sql, T Parameters, string connectionstring)
        {
            Task<IDataReader> dataReader = null;
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (IDbTransaction _dbTransaction = connection.BeginTransaction())
                {
                    try
                    {
                        dataReader = connection.ExecuteReaderAsync(sql, Parameters);
                        _dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _seriLogger.Error("Exception in DataAccess - SaveDataReader :: " + ex.Message);
                        _dbTransaction.Rollback();
                        throw;
                        //_globalLogger.Error("Event ID - {0} - Exception - {1}: ", GlobalEngine.HISEventID.GlobalEvent.GetLoggerEventId.Id, ex.Message + ex.StackTrace);
                    }
                    return dataReader;
                }
            }
        }

        public object SaveDataScalar<T, U>(string sql, U Parameters, string connectionstring)
        {
            object obj = new object();
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (IDbTransaction _dbTransaction = connection.BeginTransaction())
                {
                    try
                    {
                        obj = connection.ExecuteScalar(sql, Parameters);
                        _dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _dbTransaction.Rollback();
                        _seriLogger.Error("Exception in DataAccess - SaveDataScalar :: " + ex.Message);
                        throw;
                        //_globalLogger.Error("Event ID - {0} - Exception - {1}: ", GlobalEngine.HISEventID.GlobalEvent.GetLoggerEventId.Id, ex.Message + ex.StackTrace);
                    }
                    return obj;
                }
            }
        }
        public Task SaveDataScalarAsync<T>(string sql, T Parameters, string connectionstring)
        {
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    return connection.ExecuteScalarAsync(sql, Parameters);
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - SaveDataScalarAsync :: " + ex.Message);
                throw;
            }
        }

        public Task SaveDataScalarAsync<T, U>(string sql, U Parameters, string connectionstring)
        {
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    return connection.ExecuteScalarAsync<T>(sql, Parameters);
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - SaveDataScalarAsync :: " + ex.Message);
                throw;
            }
        }

        public int ExecuteScalarAsync<U>(string sql, U Parameters, string connectionstring)
        {
            try
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    return connection.ExecuteScalar<int>(sql, Parameters);
                }
            }
            catch (Exception ex)
            {
                _seriLogger.Error("Exception in DataAccess - ExecuteScalarAsync :: " + ex.Message);
                throw;
            }
        }




        public int SaveData<T>(T Parameters, string connectionstring, string spname)
        {
            int rows = 0;
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (IDbTransaction _dbTransaction = connection.BeginTransaction())
                {
                    try
                    {
                        rows = connection.ExecuteAsync(spname, Parameters, commandType: CommandType.StoredProcedure).Result;
                        _dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _dbTransaction.Rollback();
                        _seriLogger.Error("Exception in DataAccess - SaveData :: " + ex.Message);
                        throw;
                        //_globalLogger.Error("Event ID - {0} - Exception - {1}: ", GlobalEngine.HISEventID.GlobalEvent.GetLoggerEventId.Id, ex.Message + ex.StackTrace);
                    }
                }
            }
            return rows;
        }

        public Task SaveDataReader<T>(T Parameters, string connectionstring, string spname)
        {
            Task<IDataReader> dataReader = null;
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (IDbTransaction _dbTransaction = connection.BeginTransaction())
                {
                    try
                    {
                        dataReader = connection.ExecuteReaderAsync(spname, Parameters, commandType: CommandType.StoredProcedure);
                        _dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _dbTransaction.Rollback();
                        _seriLogger.Error("Exception in DataAccess - SaveDataReader :: " + ex.Message);
                        throw;
                        //_globalLogger.Error("Event ID - {0} - Exception - {1}: ", GlobalEngine.HISEventID.GlobalEvent.GetLoggerEventId.Id, ex.Message + ex.StackTrace);
                    }
                    return dataReader;
                }
            }
        }

        public object SaveDataScalar<T, U>(U Parameters, string connectionstring, string spname)
        {
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
                        _seriLogger.Error("Exception in DataAccess - SaveDataScalar :: " + ex.Message);
                        throw;
                        //_globalLogger.Error("Event ID - {0} - Exception - {1}: ", GlobalEngine.HISEventID.GlobalEvent.GetLoggerEventId.Id, ex.Message + ex.StackTrace);
                    }
                }
                return obj;
            }
        }

        // To insert array and return arrray
        public List<U> InsertListData<T, U>(string sql, T Parameters, string type, string connectionstring)
        {
            NpgsqlDataReader dataReader = null;
            List<U> lstId = new List<U>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue(type, Parameters);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (var _dbTransaction = connection.BeginTransaction())
                    {
                        try
                        {
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    for (int i = 0; i < dataReader.GetFieldValue<U[]>(0).Length; i++)
                                    {
                                        var id = dataReader.GetFieldValue<U[]>(0)[i];
                                        lstId.Add(id);
                                    }
                                }
                            }
                            dataReader.Close();
                            _dbTransaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            _dbTransaction.Rollback();
                            _seriLogger.Error("Exception in DataAccess - InsertListData :: " + ex.Message);
                            throw;
                        }
                    }

                }
            }
            return lstId;
        }

        // To insert multiple array and return arrray
        public List<U> InsertMultiArray<T, U>(string sql, Dictionary<object, string> d, string connectionstring)
        {
            NpgsqlDataReader dataReader = null;
            List<U> lstId = new List<U>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    foreach (KeyValuePair<object, string> types in d)
                    {
                        cmd.Parameters.AddWithValue(types.Value, types.Key);
                    }
                    //cmd.Parameters.AddWithValue(type, Parameters);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (var _dbTransaction = connection.BeginTransaction())
                    {
                        try
                        {
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    for (int i = 0; i < dataReader.GetFieldValue<U[]>(0).Length; i++)
                                    {
                                        var id = dataReader.GetFieldValue<U[]>(0)[i];
                                        lstId.Add(id);
                                    }
                                }
                            }
                            dataReader.Close();
                            _dbTransaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            _dbTransaction.Rollback();
                            _seriLogger.Error("Exception in DataAccess - InsertMultiArray :: " + ex.Message);
                            throw;
                        }
                    }

                }
            }
            return lstId;
        }

        // To insert array and return single value
        public U InsertData<T, U>(string sql, T Parameters, string type, string connectionstring)
        {
            NpgsqlDataReader dataReader = null;
            U id = default(U);
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue(type, Parameters);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (var _dbTransaction = connection.BeginTransaction())
                    {
                        try
                        {
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    id = dataReader.GetFieldValue<U>(0);
                                }
                            }
                            dataReader.Close();
                            _dbTransaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            _dbTransaction.Rollback();
                            _seriLogger.Error("Exception in DataAccess - InsertData :: " + ex.Message);
                            throw;
                        }
                    }
                }
            }
            return id;
        }
    }
}
