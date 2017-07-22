 
 
 
using System.Data.Entity;
using GeneModel;
using System.Data.Entity.Core.Objects;

namespace GeneDal
{
	public partial class GeneDataDAL : BaseDAL<GeneData>
	{
		public  GeneDataDAL(DbContext entity):base(entity)
		{
		}
	 }
	public partial class PhoneDAL : BaseDAL<Phone>
	{
		public  PhoneDAL(DbContext entity):base(entity)
		{
		}
	 }
	public partial class ProductDAL : BaseDAL<Product>
	{
		public  ProductDAL(DbContext entity):base(entity)
		{
		}
	 }
	public partial class UserDAL : BaseDAL<User>
	{
		public  UserDAL(DbContext entity):base(entity)
		{
		}
	 }
}