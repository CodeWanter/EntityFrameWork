using Common;
using GeneBll;
using GeneModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KWGene.Controllers
{
    public class FormController : Controller
    {
        private readonly CheckGuideBLL cgBll;

        public FormController()
        {
            cgBll = Container.Resolver<CheckGuideBLL>();
        }
        // GET: Form
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public Boolean ValidateCode(string Code)
       {
            //这里验证用户输入的验证码和系统的验证码是否相同
            string sessionCode = Session["ValidateCode"] == null ? new Guid().ToString() : Session["ValidateCode"].ToString();

            //将验证码去掉，避免暴力破解
            Session["ValidateCode"] = new Guid();

            if (sessionCode != Code)
                return false;
            return true;
        }

        [HttpPost]
        public ActionResult CheckGuide(CheckGuide model)
        {
            
            var excute = cgBll.AddEntity(model);
            if (excute)
                return RedirectToAction("Success");
            return RedirectToAction("Field");
        }

        public ActionResult Success()
        {
            return View();
        }

        /// <summary>
        /// 验证码的校验
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ActionResult CheckCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        public JsonResult Upload()
        {
            HttpPostedFileBase file = Request.Files["file"];
            if (file == null)
            {
                return Json(new { error = "上传异常" });
            }
            var ext = Path.GetExtension(file.FileName);
            var filename = Path.GetFileNameWithoutExtension(file.FileName);
            var serverfilenname = Guid.NewGuid().ToString("n") + "_" + filename + ext;
            try
            {
                var path = "/File";
                var dic = string.Format("{0}/{1}/{2}/{3}", path, DateTime.Today.Year.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Day.ToString());
                if (!Directory.Exists(Server.MapPath(dic)))
                {
                    Directory.CreateDirectory(Server.MapPath(dic));
                }
                var webpath = string.Format("{0}/{1}", dic, serverfilenname);
                var serverpath = Path.Combine(Server.MapPath(dic), serverfilenname);
                file.SaveAs(serverpath);
                return Json(new {
                    url = "/Form/uploaddelete",//定义要删除的action
                    key = serverfilenname,
                    path = webpath });
            }
            catch (Exception ex)
            {
                return Json(new { error = "上传异常" + ex });
            }
        }

        /// <summary>
        /// 删除 上传文件
        /// </summary>
        [HttpPost]
        public JsonResult UpLoadDelete()
        {
            try
            {
                var key = Request.Params["key"];
                var path = Request.Params["path"];
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(path))
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }
                path = Server.MapPath(path);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }

    }
}