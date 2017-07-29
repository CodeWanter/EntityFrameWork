 
 
 
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
	public partial class UserInfoDAL : BaseDAL<UserInfo>
	{
		public  UserInfoDAL(DbContext entity):base(entity)
		{
		}
	 }
}