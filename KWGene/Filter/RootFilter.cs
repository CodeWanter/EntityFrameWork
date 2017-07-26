using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KWGene.Filter
{
    public class RootFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] != null)
            {
               //string userID = filterContext.HttpContext.Session["UserID"].ToString();
                int root = 1;
                
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/Login/Login");
                return;
                //用户未登录
            }
        }
    }
}