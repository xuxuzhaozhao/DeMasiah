using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeMasiah.Common;
using System.IO;

namespace DeMasiah.Controllers
{
    public class HomeController : Controller
    {
        private readonly DAL.VideoDAL _dal = new DAL.VideoDAL();
        private readonly string _url = ConfigurationManager.AppSettings["videourl"];
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [HttpPost]
        public ActionResult Index(string url)
        {
            //string url = System.Web.HttpContext.Current.Request["url"];
            url = HttpUtility.UrlDecode(url); ;
            return Content(QRCodeCommon.CreateQRCode(url));
        }

        /// <summary>
        /// 扫描二位码查看的视频
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Video(int id)
        {
            var video = _dal.GetVideoById(id);
            ViewBag.Name = video.name;
            ViewBag.Url = video.videourl; 
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// 上传超过4MB的文件的时候，一定要在webconfig中进行配置，不然弄死进不来
        /// </summary>
        /// <param name="name">视频的名字</param>
        /// <param name="type">视频的类型</param>
        /// <returns></returns>
        public JsonResult plupload(string name, int type)
        {
            var request = System.Web.HttpContext.Current.Request;
            var videourl = string.Empty;
            var chunk = Convert.ToInt32(Request["chunk"]);//当前的分块
            var chunks = Convert.ToInt32(Request["chunks"]);//总的分块数

            var videoname = HttpUtility.UrlDecode(Request["name"]);//避免出现中文乱码

            var savepath = $"/VIDEO_FILE/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/";

            foreach (string upload in Request.Files)
            {
                if (upload != null && upload.Trim() != "")
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + savepath;
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    HttpPostedFileBase postFile = Request.Files[upload];
                    string ext = Path.GetExtension(postFile.FileName);
                    string filename = Path.GetFileName(postFile.FileName);

                    string newFileName = $"{videoname}_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{ext}";
                    if (chunks > 1) newFileName = $"{chunk}_{filename}";

                    string fileNamePath = path + newFileName;
                    postFile.SaveAs(fileNamePath);

                    videourl = $"http://{_url}{savepath + newFileName}";

                    if (chunks > 1 && chunk + 1 == chunks)
                    {
                        string newFileName1 = $"{videoname}_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.mp4";
                        using(FileStream fs=new FileStream(path + newFileName1, FileMode.Create, FileAccess.Write))
                        {
                            BinaryWriter bw = new BinaryWriter(fs);

                            for(int i = 0; i < chunks; i++)
                            {
                                bw.Write(System.IO.File.ReadAllBytes(path + i.ToString() + "_" + filename));
                                System.IO.File.Delete(path + i.ToString() + "_" + filename);        //删除指定文件信息
                                bw.Flush(); //清理缓冲区
                            }
                        }
                        videourl= $"http://{_url}{savepath + newFileName1}";
                    }
                }
            }

            //在进行大文件上传的时候，分片是没有后缀名的，必须要有后缀名才存到数据库当中。
            if(videourl.Contains(".mp4"))
            {
                var model = new Models.Video()
                {
                    Type = type,
                    Name = videoname,
                    VideoUrl = videourl,
                    CreateTime = DateTime.Now,
                    Updater = 1,
                    UpDateTime = DateTime.Now,
                    Creator = 1
                };
                _dal.AddVideo(model, _url);
            }

            return Json(new { staus = 1, message = "上传成功。" });
        }
    }
}
