using GeneBll;
using GeneModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KWGene.Areas.AdminArea.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserInfoBLL  userBll;

        public AccountController()
        {
            userBll = Container.Resolver<UserInfoBLL>();
        }
        // GET: AdminArea/Account
        public ActionResult  Login()
        {
            UserInfo model = new UserInfo()
            {
                UserName = "1",
                Password = "1",
                Createtime=DateTime.Now,
                Role="admin",
                Root="1|2|3|4"
            };
            //userBll.AddEntity(model);
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName,string psw)
        {
            UserInfo model = userBll.GetEntity(p => p.UserName == userName && p.Password == psw);
            if (model!=null)
            {
                Session["userID"] = model.Id;
               
                return RedirectToAction("Index", "Default",new { area = "AdminArea" , currentUsername = userName });
            }
            return RedirectToAction("Login", "Account", new { area = "AdminArea" });
        }
    }
}