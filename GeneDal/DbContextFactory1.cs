 
 
 
using System.Data.Entity;
using GeneModel;

namespace GeneBll
{
	public partial class DbContextFactory:DbContext
    {
		public DbContextFactory() : base("KWGENEEntities") { 
		//Database.SetInitializer<DbContextFactory>(null);
   }

     		public DbSet<GeneData> GeneData { get; set; }
					public DbSet<Phone> Phone { get; set; }
					public DbSet<Product> Product { get; set; }
					public DbSet<User> User { get; set; }
			}

}