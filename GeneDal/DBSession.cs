 
 
 
using GeneModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;

namespace GeneDal
{
    /// <summary>
    /// 数据仓储管理类
    /// DBSession管理着所有操作数据库的对象(dbContext、DAL对象等)
    /// </summary>
    public class DBSession
    {
        readonly DbContext entity = DbContextFactory.Create(); //数据操作对象

        /// <summary>
        /// 执行sql语句，返回受影响的行数
        /// </summary>
        public int ExecuteSQL(string sql, params SqlParameter[] pars)
        {
            return entity.Database.ExecuteSqlCommand(sql, pars);
        }

        /// <summary>
        /// 执行sql语句，返回结果集
        /// </summary>
        public List<T> ExecuteStoreQuery<T>(string sql,params SqlParameter[] pars)
        {
            return pars == null ? entity.Database.SqlQuery<T>(sql).ToList() : entity.Database.SqlQuery<T>(sql, pars).ToList();
        }

        /// <summary>
        /// 执行sql语句，返回结果集
        /// </summary>
        public List<T> ExecuteStoreQuery<T>(string sql, SqlParameter[] pars, string orderBy, int pageIndex, int pageSize, out int totalCount ,string orderDirector = "asc")
        {
            SqlParameter[] clonedpars = null;
            if (pars != null)
            {
                clonedpars = new SqlParameter[pars.Length];
                for (int i = 0, j = pars.Length; i < j; i++)
                {
                    clonedpars[i] = (SqlParameter)((ICloneable)pars[i]).Clone();
                }
            }
            totalCount = pars == null ?entity.Database.SqlQuery<T>(sql).Count() : entity.Database.SqlQuery<T>(sql, pars).Count();
            int num1 = (pageIndex - 1) * pageSize;
            int num2 = pageIndex * pageSize;
            sql = pageIndex == 1 ? string.Format("select * from (select *,ROW_NUMBER() OVER (ORDER BY {0} {1}) rownum from ({2}) sys_temp) sys_tb where rownum<={3}", orderBy, orderDirector, sql, num2) : string.Format("select * from (select *,ROW_NUMBER() OVER (ORDER BY {0} {1}) rownum from ({2}) sys_temp) sys_tb where rownum>{3} and rownum<={4}", orderBy, orderDirector, sql, num1, num2);
            return clonedpars == null ?entity.Database.SqlQuery<T>(sql).ToList(): entity.Database.SqlQuery<T>(sql, clonedpars).ToList();
        }

        public int SaveChanges()
        {
            return entity.SaveChanges();
        }
	private ArticleListDAL _articlelistDAL;
	public ArticleListDAL ArticleListDAL
	{
		get
		{
			if (_articlelistDAL == null)
			{
				_articlelistDAL = new ArticleListDAL(entity);
			}
			return _articlelistDAL;
		}
		set
		{
			_articlelistDAL = value;
		}
	}

	private CheckGuideDAL _checkguideDAL;
	public CheckGuideDAL CheckGuideDAL
	{
		get
		{
			if (_checkguideDAL == null)
			{
				_checkguideDAL = new CheckGuideDAL(entity);
			}
			return _checkguideDAL;
		}
		set
		{
			_checkguideDAL = value;
		}
	}

	private RoleToMenusDAL _roletomenusDAL;
	public RoleToMenusDAL RoleToMenusDAL
	{
		get
		{
			if (_roletomenusDAL == null)
			{
				_roletomenusDAL = new RoleToMenusDAL(entity);
			}
			return _roletomenusDAL;
		}
		set
		{
			_roletomenusDAL = value;
		}
	}

	private WF_MenuDAL _wf_menuDAL;
	public WF_MenuDAL WF_MenuDAL
	{
		get
		{
			if (_wf_menuDAL == null)
			{
				_wf_menuDAL = new WF_MenuDAL(entity);
			}
			return _wf_menuDAL;
		}
		set
		{
			_wf_menuDAL = value;
		}
	}

	private WF_RoleDAL _wf_roleDAL;
	public WF_RoleDAL WF_RoleDAL
	{
		get
		{
			if (_wf_roleDAL == null)
			{
				_wf_roleDAL = new WF_RoleDAL(entity);
			}
			return _wf_roleDAL;
		}
		set
		{
			_wf_roleDAL = value;
		}
	}

	private WF_UserDAL _wf_userDAL;
	public WF_UserDAL WF_UserDAL
	{
		get
		{
			if (_wf_userDAL == null)
			{
				_wf_userDAL = new WF_UserDAL(entity);
			}
			return _wf_userDAL;
		}
		set
		{
			_wf_userDAL = value;
		}
	}

 private BaseDAL<SqlCommonModel> _CRFSqlCommonDAL;
        public BaseDAL<SqlCommonModel> CRFSqlCommonDAL
        {
            get
            {
                if (_CRFSqlCommonDAL == null)
                {
                    _CRFSqlCommonDAL = new BaseDAL<SqlCommonModel>(entity);
                }
                return _CRFSqlCommonDAL;
            }
            set
            {
                _CRFSqlCommonDAL = value;
            }
        }

	  private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entity.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
	}
}