using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using GeneModel;

namespace GeneDal
{
    public partial class DbContextFactory
    {
        public static DbContext Create()
        {
            KWGENEEntities dbContext = CallContext.GetData("DbContext") as KWGENEEntities;
            if (dbContext == null)
            {
                dbContext = new KWGENEEntities();
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;
        }
    }
}
