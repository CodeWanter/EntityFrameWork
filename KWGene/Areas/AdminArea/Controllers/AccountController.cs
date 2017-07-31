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

        /// <summary>
        /// 验证用户名密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public int denglu(string username, string password)
        {
            UserInfo model = userBll.GetEntity(p => p.UserName == username && p.Password == password);
            if (model != null)
            {
                Session["userID"] = model.Id;
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}