using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneDal
{
    public class BigDataSaveHelper
    {
        private SqlConnection Conn = null;

        /// <summary>
        /// 数据库链接字符串存储在Webconfig里面(名称:KWGENE).
        /// </summary>
        public BigDataSaveHelper()
        {
            string strConn = ConfigurationManager.ConnectionStrings["KWGENE"].ConnectionString;
            Conn = new SqlConnection(strConn);
        }

        /// <summary>
        /// 事物在调用的地方创建,并且每个都传进来,调用时加上<select IDENT_CURRENT('CRFData')>,这样会返回自增列的值.
        /// </summary>
        public int ExecSQLReturnID(string sql, SqlTransaction trans)
        {
            return ExecSQLReturnID(sql, null, trans);
        }
        public int ExecSQLReturnID(string sql, SqlParameter[] Params, SqlTransaction trans)
        {
            int id = -1;
            SqlCommand command = GetConn().CreateCommand();
            command.Transaction = trans;
            command.CommandText = sql;
            if (Params != null)
                command.Parameters.AddRange(Params);
            SqlDataReader read = command.ExecuteReader();
            if (read.Read())
            {
                id = int.Parse(read[0].ToString());
            }
            read.Close();

            return id;
        }

        /// <summary>
        /// 事物在调用的地方创建,并且每个都传进来,并返回受影响的行数.
        /// </summary>
        public int ExecSQL(string sql, SqlTransaction trans)
        {
            return ExecSQL(sql, null, trans);
        }
        public int ExecSQL(string sql, SqlParameter[] Params, SqlTransaction trans)
        {
            SqlCommand command = GetConn().CreateCommand();
            command.Transaction = trans;
            command.CommandText = sql;
            if (Params != null)
                command.Parameters.AddRange(Params);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 将DataTable中的数据复制到数据库.
        /// </summary>
        public void BigSave(DataTable tb, string tbName, SqlTransaction trans)
        {
            SqlBulkCopy copy = new SqlBulkCopy(GetConn(), SqlBulkCopyOptions.CheckConstraints, trans)
            {
                DestinationTableName = tbName,
                BatchSize = tb.Rows.Count
            };
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                copy.ColumnMappings.Add(tb.Columns[i].ColumnName, tb.Columns[i].ColumnName);
            }
            copy.WriteToServer(tb);
        }

        /// <summary>
        /// 将DataTable中的数据复制到数据库.
        /// </summary>
        public void BigSave(DataTable tb, string tbName)
        {
            using (SqlBulkCopy copy = new SqlBulkCopy(GetConn()))
            {

                copy.DestinationTableName = tbName;
                copy.BatchSize = tb.Rows.Count;
                for (int i = 0; i < tb.Columns.Count; i++)
                {
                    copy.ColumnMappings.Add(tb.Columns[i].ColumnName, tb.Columns[i].ColumnName);
                }
                copy.WriteToServer(tb);
                GetConn().Close();
            }

        }

        /// <summary>
        /// 获取数据库链接.
        /// </summary>
        public SqlConnection GetConn()
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            return Conn;
        }

        /// <summary>
        /// 关闭链接.
        /// </summary>
        public void CloseConn()
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
}
