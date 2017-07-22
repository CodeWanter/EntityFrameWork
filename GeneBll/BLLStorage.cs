 
 
 
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
			            bulider.RegisterType<GeneDataBLL >();
			            bulider.RegisterType<PhoneBLL >();
			            bulider.RegisterType<ProductBLL >();
			            bulider.RegisterType<UserBLL >();
			            container = bulider.Build();
        }
        
    }
	public partial class GeneDataBLL : BaseBLL<GeneData>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.GeneDataDAL;
		}
	}
	public partial class PhoneBLL : BaseBLL<Phone>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.PhoneDAL;
		}
	}
	public partial class ProductBLL : BaseBLL<Product>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.ProductDAL;
		}
	}
	public partial class UserBLL : BaseBLL<User>
	{
		public override void SetCurrentDAL()
		{
			CurrentDAL = DbSession.UserDAL;
		}
	}

}