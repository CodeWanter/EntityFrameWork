using GeneBll;
using GeneModel;
using KWGene.Filter;
using KWGene.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KWGene.Areas.AdminArea.Controllers
{
    public class AccountController : Controller
    {
        private readonly WF_UserBLL userBll;
        private readonly WF_RoleBLL roleBll;
        private readonly WF_MenuBLL menuBll;
        private readonly RoleToMenusBLL rtBll;

        public AccountController()
        {
            roleBll = Container.Resolver<WF_RoleBLL>();
            userBll = Container.Resolver<WF_UserBLL>();
            menuBll = Container.Resolver<WF_MenuBLL>();
            rtBll = Container.Resolver<RoleToMenusBLL>();
        }
        #region 用户管理
        //用户管理页面加载
        [RootFilter]
        public ActionResult UserManage(string TrueName = "")
        {
            List<WF_User> model = new List<WF_User>();
            if (string.IsNullOrEmpty(TrueName))
            {
                model = userBll.GetList(null);
            }
            else
            {
                model = userBll.GetList(u => u.TrueName.Contains(TrueName));
            }
            return View(model);
        }
        //添加或者修改用户信息
        public ActionResult SaveUser(WF_User user)
        {
            if (user.Id != 0)
            {
                if (userBll.UpdateEntity(user))
                {
                    return Json("编辑信息成功！");
                }
                else
                {
                    return Json("修改失败！");
                }
            }
            else
            {
                if (userBll.GetCount(u => u.UserName == user.UserName) > 0)
                {
                    return Json("添加失败，用户名已存在！");
                }
                user.Createtime = DateTime.Now;//注册时间
                if (userBll.AddEntity(user))
                {
                    return Json("添加成功！");
                }
                else
                {
                    return Json("添加失败！");
                }
            }
        }
        //加载用户信息页面
        public ActionResult UpdateUser(int id)
        {
            int UserID = int.Parse(Session["userID"].ToString());
            WF_User model = new WF_User();
            if (id != 0)
                model = userBll.GetEntity(u => u.Id == id);
            WF_User user = userBll.GetEntity(u => u.Id == UserID);
            ViewBag.Roles = roleBll.GetList(null);
            return View(model);
        }
        //删除用户
        public int DelUser(List<int?> x)
        {
            foreach (var i in x)
            {
                if (userBll.ExecuteDeleteByIdWithSql("WF_User", "Id", Convert.ToInt32(i)) > 0)
                {

                }
                else
                {
                    return 2;
                }
            }
            return 1;
        }
        #endregion
        #region 角色管理
        //角色管理页面加载
        [RootFilter]
        public ActionResult RoleManage(string RoleName = "")
        {
            List<WF_Role> list = new List<WF_Role>();
            if (string.IsNullOrEmpty(RoleName))
            {
                list = roleBll.GetList(null);
            }
            else
            {
                list = roleBll.GetList(u => u.RoleName.Contains(RoleName));
            }
            return View(list);
        }
        //角色组页面加载
        public ActionResult RoleModal(int? id)
        {
            WF_Role model = new WF_Role();
            if (id != 0)
                model = roleBll.GetEntity(u => u.Id == id);
            return View(model);
        }
        //修改添加角色
        public ActionResult UpdateRole(WF_Role role)
        {
            if (role.Id != 0)
            {
                if (roleBll.UpdateEntity(role))
                {
                    return Json("编辑信息成功！");
                }
                else
                {
                    return Json("修改失败！");
                }
            }
            else
            {
                if (roleBll.GetCount(u => u.RoleName == role.RoleName) > 0)
                {
                    return Json("添加失败，角色名已存在！");
                }
                if (roleBll.AddEntity(role))
                {
                    return Json("添加成功！");
                }
                else
                {
                    return Json("添加失败！");
                }
            }
        }
        //删除角色
        public int DelRole(List<int?> x)
        {
            foreach (var i in x)
            {
                if (userBll.ExecuteDeleteByIdWithSql("WF_Role", "Id", Convert.ToInt32(i)) > 0)
                {

                }
                else
                {
                    return 2;
                }
            }
            return 1;
        }

        public JsonResult GetTreeRoot(int? id)
        {
            int UserID = int.Parse(Session["userID"].ToString());
            List<zNodes> nodesall = new List<zNodes>();
            List<zNodes> nodes = new List<zNodes>();

            List<WF_Menu> WF_Menu = menuBll.GetList(null).OrderBy(m => m.Id).ToList();
            foreach (WF_Menu item in WF_Menu)
            {
                if (item.MenuName == "权限管理"| item.MenuName == "用户管理"| item.MenuName == "角色管理")
                {
                    nodesall.Add(new zNodes()
                    {
                        name = item.MenuName,
                        id = item.Id,
                        pId = item.PId,
                        @checked = false,
                        open = false,
                        chkDisabled = true
                    });
                }else
                {
                    nodesall.Add(new zNodes()
                    {
                        name = item.MenuName,
                        id = item.Id,
                        pId = item.PId,
                        @checked = false,
                        open = false,
                        chkDisabled = false
                    });
                }
            }
            List<RoleToMenus> role = rtBll.GetList(m => m.WF_RoleId == id);
            foreach (zNodes item in nodesall)
            {
                foreach (RoleToMenus item2 in role)
                {
                    if (item.id == item2.WF_Menu.Id)
                    {
                        item.@checked = true;
                        item.open = true;
                    }
                }
            }
            return Json(nodesall, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRoots(int? id, string znodes)
        {
            List<RoleToMenus> list = new List<RoleToMenus>();
            rtBll.ExecuteDeleteByIdWithSql("RoleToMenus", "WF_RoleId", Convert.ToInt32(id));
            List<string> rootsId = znodes.Split(',').ToList();
            foreach (string item in rootsId)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    RoleToMenus model = new RoleToMenus()
                    {
                        WF_RoleId = Convert.ToInt32(id),
                        WF_MenuId = Convert.ToInt32(item)
                    };
                    list.Add(model);
                }
            }
            rtBll.AddList(list);
            return null;
        }
        public ActionResult UpdateRoot(int? id)
        {
            ViewBag.GetID = id;
            return View();
        }

        #endregion
        #region 登录后台管理
        // GET: AdminArea/Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string psw)
        {
            WF_User model = userBll.GetEntity(p => p.UserName == userName && p.Password == psw);
            if (model != null)
            {
                Session["userID"] = model.Id;
                return RedirectToAction("Index", "Default", new { area = "AdminArea", currentUsername = userName });
            }
            return RedirectToAction("Login", "Account", new { area = "AdminArea" });
        }
        //退出
        public ActionResult Exist()
        {
            Session["userID"] = null;
            return RedirectToAction("Login", "Account", new { area = "AdminArea" });
        }
        #endregion
    }
}