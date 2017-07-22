using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq.Expressions;
using GeneModel;
using System.Data.SqlClient;
using Common;

namespace GeneDal
{
    public class BaseDAL<T> where T : class
    {
        protected readonly DbContext _dbContext;

        //public BaseDAL()
        //{
        //    _dbContext = DBContextFactory.CreateContext();
        //}

        public BaseDAL(DbContext entity)
        {
            this._dbContext = entity;
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        public T GetEntity(Expression<Func<T, bool>> whereLamda)
        {
            try
            {
                return _dbContext.Set<T>().FirstOrDefault(whereLamda);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 查询符合条件的记录总数
        /// </summary>
        public int GetCount(Expression<Func<T, bool>> whereLamda)
        {
            return whereLamda != null ? _dbContext.Set<T>().Count(whereLamda) : _dbContext.Set<T>().Count();
        }

        /// <summary>
        /// 根据条件查询 返回集合
        /// </summary>
        /// <param name="whereLamda">查询条件 没有条件则为null</param>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> whereLamda)
        {
            return GetIQueryable(whereLamda).ToList();
        }


        /// <summary>
        /// 根据条件查询 返回IQueryable ,不执行查询，只有在ToLis、Select、遍历时等操作才会执行实际的查询
        /// 比如当只获取指定字段时或者获取Count时
        /// </summary>
        /// <param name="whereLamda">查询条件 没有条件则为null</param>
        /// <returns></returns>
        public IQueryable<T> GetIQueryable(Expression<Func<T, bool>> whereLamda)
        {
            if (whereLamda == null)
            {
                try
                {
                    return _dbContext.Set<T>();
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
            try
            {
                return _dbContext.Set<T>().Where(whereLamda);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 根据条件查询 返回集合
        /// </summary>
        /// <param name="whereLamda">查询条件 没有条件则为null</param>
        /// <param name="orderBy">orderBy字段，注意大小写敏感</param>
        /// <param name="isASC">是否升序</param>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> whereLamda, string orderBy, bool isASC)
        {
            var property = typeof(T).GetProperty(orderBy);
            var parameter = Expression.Parameter(typeof(T), "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var query = (_dbContext.Set<T>()).Where(item => true);
            if (whereLamda != null)
            {
                query = (_dbContext.Set<T>()).Where(whereLamda);
            }
            string methodName = isASC ? "OrderBy" : "OrderByDescending";
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
            query = query.Provider.CreateQuery<T>(resultExp);
            return query.ToList();
        }

        /// <summary>
        /// 根据条件查询 返回集合(取前count条)
        /// </summary>
        /// <param name="whereLamda">查询条件 没有条件则为null</param>
        /// <param name="orderBy">orderBy字段，注意大小写敏感</param>
        /// <param name="isASC">是否升序</param>
        /// <param name="count">取前count条</param>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> whereLamda, string orderBy, bool isASC, int count)
        {
            if (whereLamda == null)
            {
                var property = typeof(T).GetProperty(orderBy);
                var parameter = Expression.Parameter(typeof(T), "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                var query = (_dbContext.Set<T>()).Where(item => true);
                string methodName = isASC ? "OrderBy" : "OrderByDescending";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                query = query.Provider.CreateQuery<T>(resultExp);
                return query.Take(count).ToList();
            }
            else
            {
                var property = typeof(T).GetProperty(orderBy);
                var parameter = Expression.Parameter(typeof(T), "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                var query = (_dbContext.Set<T>()).Where(whereLamda);
                string methodName = isASC ? "OrderBy" : "OrderByDescending";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                query = query.Provider.CreateQuery<T>(resultExp);

                return query.Take(count).ToList();
            }
        }

        public List<T> GetListByPage(Expression<Func<T, bool>> whereLamda, string orderBy, bool isASC, int pageIndex, int pageSize, out int totalCount)
        {
            int begin = (pageIndex - 1) * pageSize;
            if (whereLamda == null)
            {
                totalCount = _dbContext.Set<T>().Count();

                var property = typeof(T).GetProperty(orderBy);
                var parameter = Expression.Parameter(typeof(T), "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                var query = (_dbContext.Set<T>()).Where(item => true);
                string methodName = isASC ? "OrderBy" : "OrderByDescending";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                query = query.Provider.CreateQuery<T>(resultExp);
                return query.Skip(begin).Take(pageSize).ToList();
            }
            else
            {
                totalCount = _dbContext.Set<T>().Where(whereLamda).Count();

                var property = typeof(T).GetProperty(orderBy);
                var parameter = Expression.Parameter(typeof(T), "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                var query = (_dbContext.Set<T>()).Where(whereLamda);
                string methodName = isASC ? "OrderBy" : "OrderByDescending";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));

                query = query.Provider.CreateQuery<T>(resultExp);
                return query.Skip(begin).Take(pageSize).ToList();

            }
        }

        public bool AddEntity(T obj)
        {
            _dbContext.Set<T>().Add(obj);
            try
            {
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int AddEntityWithReturnId(T obj, string idStr)
        {
            _dbContext.Set<T>().Add(obj);
            try
            {
                _dbContext.SaveChanges();
                return Convert.ToInt32(typeof(T).GetProperty(idStr, typeof(int)).GetValue(obj));
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddList(IEnumerable<T> list)
        {
            if (list != null)
            {
                foreach (var obj in list)
                {
                    _dbContext.Set<T>().Add(obj);
                }
            }
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public bool DeleteEntity(T obj)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Attach(obj);
            dbSet.Remove(obj);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 使用IN批量删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="parts">IN参数</param>
        public void DeleteByIdWithSql(string tableName, string columnName, int id)
        {
            var sql = string.Format("DELETE FROM  {0} WHERE {1} = {2}", tableName, columnName, id);
            _dbContext.Database.ExecuteSqlCommand(sql);
        }

        public void DeleteWithSql(string sqlStr, params SqlParameter[] parameters)
        {
            _dbContext.Database.ExecuteSqlCommand(sqlStr, parameters);
        }

        public int DeleteList(List<T> list)
        {
            if (list != null && list.Count > 0)
            {
                list = list.Distinct().ToList();
                var dbSet = _dbContext.Set<T>();
                foreach (var obj in list)
                {
                    dbSet.Attach(obj);
                    dbSet.Remove(obj);
                }
            }
            return _dbContext.SaveChanges();
        }

        public bool UpdateEntity(T obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            try
            {
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 批量修改 testing
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int UpdateList(List<T> list)
        {
            if (list != null && list.Count > 0)
            {
                list = list.Distinct().ToList();
                foreach (var obj in list)
                {
                    _dbContext.Entry(obj).State = EntityState.Modified;
                }
            }
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public bool UpdateEntity(T obj, List<string> listField, bool isUptFiled)
        {
            var stateEntry = _dbContext.Entry(obj);
            var listAllFiled = stateEntry.OriginalValues.PropertyNames;
            if (isUptFiled)//只更新这些字段
            {
                foreach (string t in listField)
                {
                    stateEntry.Property(t).IsModified = true;
                }
            }
            else//不更新这些字段
            {
                foreach (string t in from t in listAllFiled let isUpdate = listField.All(t1 => t != t1) where isUpdate select t)
                {
                    stateEntry.Property(t).IsModified = true;
                }
            }
            return _dbContext.SaveChanges() > 0;
        }

        // 返回当前数据库系统时间
        public DateTime? GetSQLServerSystemDateTime()
        {
            List<DateTime> dateList = _dbContext.Database.SqlQuery<DateTime>("select getdate()").ToList<DateTime>();
            if (dateList.Count > 0)
            {
                return dateList[0];
            }
            return null;
        }

        // 执行SQL语句(测试中的方法，暂时不要使用)
        public List<T> ExecuteSQL(string sql, params SqlParameter[] parms)
        {
            return parms != null ? _dbContext.Database.SqlQuery<T>(sql, parms).ToList() : _dbContext.Database.SqlQuery<T>(sql).ToList();
        }

        /// <summary>
        /// 直接执行Sql 返回受影响的行数。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public int ExecuteStoreCommand(string sql, params SqlParameter[] parms)
        {
            return _dbContext.Database.ExecuteSqlCommand(sql, parms);
        }
    }
}
