using GeneDal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace GeneBll
{
    public abstract class BaseBLL<T> where T : class
    {

        private DBSession _dbSession;
        public DBSession DbSession
        {
            get
            {
                if (_dbSession != null) return _dbSession;
                return _dbSession = new DBSession();
            }
        }

        public BaseDAL<T> CurrentDAL { get; set; }

        public abstract void SetCurrentDAL();

        protected BaseBLL()
        {
            SetCurrentDAL();
        }

        /// <summary>
        /// 查询一个实体
        /// </summary>
        public virtual T GetEntity(Expression<Func<T, bool>> whereLamda)
        {
            return CurrentDAL.GetEntity(whereLamda);
        }

        /// <summary>
        /// 查询符合条件的记录总数
        /// </summary>
        //public virtual int GetCount(Func<T, bool> whereLamda) {
        //    return CurrentDAL.GetCount(whereLamda);
        //}
        public virtual int GetCount(Expression<Func<T, bool>> whereLamda)
        {
            return CurrentDAL.GetCount(whereLamda);
        }
        /// <summary>
        /// 根据条件查询 返回集合
        /// </summary>
        /// <param name="whereLamda">查询条件 没有条件则为null</param>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> whereLamda)
        {
            return CurrentDAL.GetList(whereLamda);
        }

        /// <summary>
        /// 根据条件查询 返回集合
        /// </summary>
        /// <param name="whereLamda">查询条件 没有条件则为null</param>
        /// <param name="orderBy">orderBy字段，注意大小写敏感</param>
        /// <param name="isASC">是否升序</param>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> whereLamda, string orderBy, bool isASC)
        {
            return CurrentDAL.GetList(whereLamda, orderBy, isASC);
        }

        /// <summary>
        /// 根据条件查询 返回集合
        /// </summary>
        /// <param name="whereLamda">查询条件 没有条件则为null</param>
        /// <param name="orderBy">orderBy字段，注意大小写敏感</param>
        /// <param name="isASC">是否升序</param>
        /// <param name="count">取前count条</param>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> whereLamda, string orderBy, bool isASC, int count)
        {
            return CurrentDAL.GetList(whereLamda, orderBy, isASC, count);
        }

        /// <summary>
        /// 根据条件查询 返回集合(分页)
        /// </summary>
        /// <param name="whereLamda">查询条件 查询所有，则为null</param>
        /// <param name="orderbyLamda">排序字段，如:UserID 注意大小写敏感</param>
        /// <param name="isASC">是否是升序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public virtual List<T> GetListByPage(Expression<Func<T, bool>> whereLamda, string orderby, bool isASC, int pageIndex, int pageSize, out int totalCount)
        {
            return CurrentDAL.GetListByPage(whereLamda, orderby, isASC, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 新增
        /// </summary>
        public virtual bool AddEntity(T obj)
        {
            return CurrentDAL.AddEntity(obj);
        }

        /// <summary>
        /// 新增
        /// </summary>
        public virtual int AddEntityWithReturnId(T obj, String idStr)
        {
            return CurrentDAL.AddEntityWithReturnId(obj, idStr);
        }
        // 批量添加
        public virtual int AddList(IEnumerable<T> list)
        {
            return CurrentDAL.AddList(list);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public virtual bool DeleteEntity(T obj)
        {
            return CurrentDAL.DeleteEntity(obj);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int DeleteList(List<T> list)
        {
            return CurrentDAL.DeleteList(list);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public virtual bool UpdateEntity(T obj)
        {
            return CurrentDAL.UpdateEntity(obj);
        }

        // 批量修改
        public virtual int UpdateList(List<T> list)
        {
            return CurrentDAL.UpdateList(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        public virtual bool UpdateEntity(T obj, List<string> listField, bool IsUptFiled)
        {
            return CurrentDAL.UpdateEntity(obj, listField, IsUptFiled);
        }

        /// <summary>
        /// 返回当前数据库系统时间
        /// </summary>
        public DateTime GetSQLServerSystemDateTime()
        {
            DateTime? now = CurrentDAL.GetSQLServerSystemDateTime();
            if (now == null)
            {
                return DateTime.Now;
            }
            return (DateTime)now;
        }

        public void ExecuteBulkInsert(string destinationTableName, DataTable sourcedt)
        {
            BigDataSaveHelper bigDataSaveHelper = new BigDataSaveHelper();
            bigDataSaveHelper.BigSave(sourcedt, destinationTableName);
        }

        public int ExecuteDeleteByIdWithSql(string destinationTableName, string columnName, int id)
        {
           return CurrentDAL.DeleteByIdWithSql(destinationTableName, columnName, id);
        }

        public void ExecuteDeleteWithSql(string sqlStr, params SqlParameter[] parameters)
        {
            CurrentDAL.DeleteWithSql(sqlStr, parameters);
        }

        //public ObjectResult<T> ExecuteSQL(string sql, params SqlParameter[] parms) {
        //    return CurrentDAL.ExecuteSQL(sql, parms);
        //}
    }
}
