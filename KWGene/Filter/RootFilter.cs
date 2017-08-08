using Autofac.Core;
using GeneBll;
using GeneModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KWGene.Filter
{
    public class RootFilter : ActionFilterAttribute, IActionFilter
    {
        private readonly WF_UserBLL userBll;
        private readonly WF_MenuBLL menuBll;
        private readonly RoleToMenusBLL rtBll;
        public RootFilter()
        {
            userBll = GeneBll.Container.Resolver<WF_UserBLL>();
            menuBll = GeneBll.Container.Resolver<WF_MenuBLL>();
            rtBll = GeneBll.Container.Resolver<RoleToMenusBLL>();
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] != null)
            {
                int userID = Int32.Parse(filterContext.HttpContext.Session["UserID"].ToString());
                WF_User model = userBll.GetEntity(p => p.Id == userID);
               
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/AdminArea/Account/Login");
                return;
                //用户未登录
            }
        }
    }
}