 
 
 
using GeneModel;
using Autofac;

namespace GeneBll
{
 public class Container {
        public static IContainer container = null;

        public static T Resolver<T>()
        {
            try
            {
                if (container == null)
                {
                    Initialise();
                }
            }
            catch (System.Exception ex) {
                throw new System.Exception("IOC实例化出错！" + ex.Message);
            }
            return container.Resolve<T>();
        }
        public static void Initialise() {
            var bulider = new ContainerBuilder();
			            bulider.RegisterType<ArticleListBLL >();
			            bulider.RegisterType<CheckGuideBLL >();
			            bulider.RegisterType<MenuDetailBLL >();
			            bulider.RegisterType<MenuInfoBLL >();
			            bulider.RegisterType<UserInfoBLL >();
			            container = bulider.Build();
        }
        
    }
	public partial class ArticleListBLL : BaseBLL<ArticleList>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.ArticleListDAL;
		}
	}
	public partial class CheckGuideBLL : BaseBLL<CheckGuide>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.CheckGuideDAL;
		}
	}
	public partial class MenuDetailBLL : BaseBLL<MenuDetail>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.MenuDetailDAL;
		}
	}
	public partial class MenuInfoBLL : BaseBLL<MenuInfo>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.MenuInfoDAL;
		}
	}
	public partial class UserInfoBLL : BaseBLL<UserInfo>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.UserInfoDAL;
		}
	}

}