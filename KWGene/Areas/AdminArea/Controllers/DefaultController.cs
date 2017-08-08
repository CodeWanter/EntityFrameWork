using GeneBll;
using GeneModel;
using KWGene.Filter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KWGene.Areas.AdminArea.Controllers
{
    public class DefaultController : Controller
    {
        private readonly WF_UserBLL userBll;
        private readonly WF_RoleBLL roleBll;
        private readonly WF_MenuBLL menuBll;
        private readonly RoleToMenusBLL rtBll;

        public DefaultController()
        {
            roleBll = Container.Resolver<WF_RoleBLL>();
            userBll = Container.Resolver<WF_UserBLL>();
            menuBll = Container.Resolver<WF_MenuBLL>();
            rtBll = Container.Resolver<RoleToMenusBLL>();
        }
        // GET: AdminArea/Home
        public ActionResult Index()
        {
            List<WF_Menu> list = new List<WF_Menu>();

            if (Session["userID"] != null)
            {
                int Userid = Int32.Parse(Session["userID"].ToString());
                WF_User model = userBll.GetEntity(p => p.Id == Userid);
                List<RoleToMenus> role = rtBll.GetList(m => m.WF_RoleId == model.WF_RoleId);
                role = role.OrderBy(p => p.WF_MenuId).ToList<RoleToMenus>();
                foreach (RoleToMenus item in role)
                {
                    WF_Menu menu = item.WF_Menu;
                    if (menu.PId != 0)
                    {
                        WF_Menu submenu = menuBll.GetEntity(m => m.Id == menu.PId);
                        submenu.SubList.Add(menu);
                    }
                }
                foreach (RoleToMenus item in role)
                {
                    WF_Menu menu = item.WF_Menu;
                    if (menu.PId == 0)
                    {
                        list.Add(menu);
                    }
                }
                ViewBag.Role = model.WF_Role.RoleName;
                ViewBag.UserName = model.TrueName;
            }
            return View(list);
        }
    }
}