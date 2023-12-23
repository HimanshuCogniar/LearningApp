using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace LearningAPI.Repositories
{
    public interface IDataAccess
    {

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        Task<dynamic> LoadData<U>(string sql, U Parameters, string connectionstring);

        public List<T> LoadDataSync<T, U>(string sql, U Parameters, string connectionstring);

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        Task<List<T>> LoadData<T, U>(string sql, U Parameters, string connectionstring);

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        Task<List<T>> LoadMasterData<T>(string sql, string connectionstring, string methodName);

        [Description("Method to retrieve data using SQL query"), Category("DataAccess")]
        Task<T> LoadTypeData<T>(string sql, string connectionstring);


        [Description("Method to retrieve data using Stored Procedure"), Category("DataAccess")]
        Task<List<T>> LoadSingleData<T, U>(string sql, U Parameters, string connectionstring);

        T LoadSingleRecord<T, U>(string sql, U Parameters, string connectionstring);

        [Description("Method to retrieve data using Stored Procedure"), Category("DataAccess")]
        Task<List<T>> LoadData<T, U>(string sql, U Parameters, string connectionstring, string spname);

        Task<List<T>> LoadMultipleData<T, T1, T2, U>(string sql, U Parameters, string connectionstring);

        Task<List<T>> LoadMultipleData<T, T1, T2, T3, U>(string sql, U Parameters, string connectionstring);
        Task<List<T>> LoadMultipleData<T, T1, T2, T3, T4, U>(string sql, U Parameters, string connectionstring);

        int SaveData<T>(string sql, T Parameters, string connectionstring);

        Task SaveDataReader<T>(string sql, T Parameters, string connectionstring);

        object SaveDataScalar<T, U>(string sql, U Parameters, string connectionstring);

        Task SaveDataScalarAsync<T>(string sql, T Parameters, string connectionstring);
        Task SaveDataScalarAsync<T, U>(string sql, U Parameters, string connectionstring);

        int ExecuteScalarAsync<U>(string sql, U Parameters, string connectionstring);

        int SaveData<T>(T Parameters, string connectionstring, string spname);

        Task SaveDataReader<T>(T Parameters, string connectionstring, string spname);

        object SaveDataScalar<T, U>(U Parameters, string connectionstring, string spname);
        List<U> InsertListData<T, U>(string sql, T Parameters, string type, string connectionstring);
        U InsertData<T, U>(string sql, T Parameters, string type, string connectionstring);
        List<U> InsertMultiArray<T, U>(string sql, Dictionary<object, string> d, string connectionstring);
    }
}
