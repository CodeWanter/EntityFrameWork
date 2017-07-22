using GeneDal;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GeneBll
{
    public class SqlCommonBLL<T>
    {
        SqlCommonDAL<T> dal = new SqlCommonDAL<T>();

        /// <summary>
        /// 执行SQL返回一个集合
        /// </summary>
        public List<T> ExecuteStoreQuery(string sql, params SqlParameter[] pars)
        {
            return dal.ExecuteStoreQuery(sql, pars);
        }

        public int GetCountByColumnWithSql(string tableName, string columnName, string value)
        {
            return dal.GetCountByColumnWithSql(tableName,columnName, value);
        }

    }
}
