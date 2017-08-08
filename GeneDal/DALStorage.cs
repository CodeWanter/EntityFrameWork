 
 
 
using System.Data.Entity;
using GeneModel;
using System.Data.Entity.Core.Objects;

namespace GeneDal
{
	public partial class ArticleListDAL : BaseDAL<ArticleList>
	{
		public  ArticleListDAL(DbContext entity):base(entity)
		{
		}
	 }
	public partial class CheckGuideDAL : BaseDAL<CheckGuide>
	{
		public  CheckGuideDAL(DbContext entity):base(entity)
		{
		}
	 }
	public partial class RoleToMenusDAL : BaseDAL<RoleToMenus>
	{
		public  RoleToMenusDAL(DbContext entity):base(entity)
		{
		}
	 }
	public partial class WF_MenuDAL : BaseDAL<WF_Menu>
	{
		public  WF_MenuDAL(DbContext entity):base(entity)
		{
		}
	 }
	public partial class WF_RoleDAL : BaseDAL<WF_Role>
	{
		public  WF_RoleDAL(DbContext entity):base(entity)
		{
		}
	 }
	public partial class WF_UserDAL : BaseDAL<WF_User>
	{
		public  WF_UserDAL(DbContext entity):base(entity)
		{
		}
	 }
}