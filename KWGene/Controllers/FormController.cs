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
                var path ="/File";
                var dic = string.Format("{0}/{1}/{2}/{3}", path, DateTime.Today.Year.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Day.ToString());
                if (!Directory.Exists(Server.MapPath(dic)))
                {
                    Directory.CreateDirectory(Server.MapPath(dic));
                }
                var webpath = string.Format("{0}/{1}", dic, serverfilenname);
                var serverpath = Path.Combine(Server.MapPath(dic), serverfilenname);
                file.SaveAs(serverpath);
                //UploadResponse res = new UploadResponse();
                //var PreView = GetPreview(ext, webpath);
                return Json(new { path = webpath });
            }
            catch(Exception ex) 
            {
                return Json(new { error = "上传异常" +ex});
            }
        }
    }
}