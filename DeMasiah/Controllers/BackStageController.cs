using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeMasiah.Controllers
{
    /// <summary>
    /// 后台管理（成员管理，视频上传，文件上传）
    /// </summary>
    public class BackStageController : Controller
    {
        // GET: BackStage
        public ActionResult Index()
        {
            return View();
        }
    }
}