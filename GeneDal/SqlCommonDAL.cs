using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneDal
{
    /// <summary>
    /// 执行SQL通用类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlCommonDAL<T>
    {
        readonly DbContext entity = DbContextFactory.Create(); //数据操作对象
        /// <summary>
        /// 执行sql语句，返回结果集
        /// </summary>
        public List<T> ExecuteStoreQuery(string sql, params SqlParameter[] pars)
        {
            return pars == null ? entity.Database.SqlQuery<T>(sql).ToList() : entity.Database.SqlQuery<T>(sql, pars).ToList();
        }

        public int GetCountByColumnWithSql(string tableName, string columnName, string value)
        {
            string connStr = ConfigurationManager.ConnectionStrings["KWGENE"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT COUNT(*) AS [NUM] FROM {0} WHERE {1} = {2}", tableName, columnName,
                        value);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (int)reader["NUM"];
                        }
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// 通用的无返回的连接
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="times">执行超时时间</param>
        public void GeneralSql(string sql, int times)
        {
            string connStr = ConfigurationManager.ConnectionStrings["KWGENE"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = times;
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        conn.Close();
                    }

                }
            }
        }
    }
}
