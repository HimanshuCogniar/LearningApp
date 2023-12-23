//using CMS.RR.Panchnama.Respository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearningAPI.Repositories
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
        Task<dynamic> FetchDataAsync<U>(string sqlGetData, U Parameters, string myConnectionString);
        Task<IEnumerable<T>> FetchDataAsyncc<T, U>(string sql, U parameters, string connectionString, string commandType);
        List<T1> FetchDataSync<T1, U>(string sqlGetData, U Parameters, string myConnectionString);
        Task<List<T>> FetchDataAsync<T, U>(string sqlGetData, U Parameters, string myConnectionString);
        Task<List<T>> FetchDataAsync<T>(string sqlGetData, string myConnectionString, string methodName);
        Task<List<T>> FetchMultipleAsync<T, T1, T2, U>(string sqlGetData, U Parameters, string myConnectionString);
        Task<List<T>> FetchMultipleAsync<T, T1, T2, T3, U>(string sqlGetData, U Parameters, string myConnectionString);
        Task<List<T>> FetchMultipleAsync<T, T1, T2, T3, T4, U>(string sqlGetData, U Parameters, string myConnectionString);
    }
}
