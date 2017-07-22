using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Data.Entity;

namespace KWGene
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
           // Database.SetInitializer<GeneModel.KWGENEEntities>(new CreateDatabaseIfNotExists<GeneModel.KWGENEEntities>());
            Database.SetInitializer<GeneModel.KWGENEEntities>(new DropCreateDatabaseIfModelChanges<GeneModel.KWGENEEntities>());

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer<DbContext>(null);
        }
    }
}