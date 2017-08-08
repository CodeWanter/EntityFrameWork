 
 
 
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
			            bulider.RegisterType<RoleToMenusBLL >();
			            bulider.RegisterType<WF_MenuBLL >();
			            bulider.RegisterType<WF_RoleBLL >();
			            bulider.RegisterType<WF_UserBLL >();
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
	public partial class RoleToMenusBLL : BaseBLL<RoleToMenus>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.RoleToMenusDAL;
		}
	}
	public partial class WF_MenuBLL : BaseBLL<WF_Menu>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.WF_MenuDAL;
		}
	}
	public partial class WF_RoleBLL : BaseBLL<WF_Role>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.WF_RoleDAL;
		}
	}
	public partial class WF_UserBLL : BaseBLL<WF_User>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.WF_UserDAL;
		}
	}

}