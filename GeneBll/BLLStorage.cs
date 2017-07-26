 
 
 
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

}