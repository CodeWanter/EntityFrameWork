using GeneBll;
using GeneModel;
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
        private readonly UserInfoBLL userBll;
        private readonly MenuInfoBLL menuBll;
        private readonly MenuDetailBLL menudBll;

        public DefaultController()
        {
            userBll = Container.Resolver<UserInfoBLL>();
            menuBll = Container.Resolver<MenuInfoBLL>();
            menudBll = Container.Resolver<MenuDetailBLL>();
        }
        // GET: AdminArea/Home
        public ActionResult Index()
        {
   
            if (Session["userID"] != null)
            {
                int Userid = Int32.Parse(Session["userID"].ToString());
                UserInfo model = userBll.GetEntity(p => p.Id == Userid);
                ViewBag.Role = model.Role;
                List<string> root = model.Root.Split('|').ToList();
                ViewBag.root = root;
            }
            return View();
        }

        public ActionResult UserList()
        {
            return View();
        }

        /// <summary>
        /// 查询出数据显示在菜单栏目中
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadMenuData()
        {
            int Userid = Int32.Parse(Session["userID"].ToString());

            List<MenuInfo> data = menuBll.GetList(p => p.UserInfoId == Userid);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}