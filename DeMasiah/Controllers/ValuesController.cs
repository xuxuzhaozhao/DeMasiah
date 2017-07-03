using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DeMasiah.Common;

namespace DeMasiah.Controllers
{
    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        private readonly string _videourl = ConfigurationManager.AppSettings["videourl"].ToString();
        private readonly DAL.VideoDAL _dal = new DAL.VideoDAL();

        [HttpGet]
        [Route("getVideosOptions")]
        public object GetVideos()
        {
            
            //List<optionsValue> options = new List<optionsValue>()
            //{
            //    new optionsValue(){name="这个视频的名字第一集",url = $"http://{_videourl}/Video/1.mp4"},
            //    new optionsValue(){name="这个视频的名字第二集",url = $"http://{_videourl}/Video/4.mp4"},
            //    new optionsValue(){name="这个视频的名字第三集",url = $"http://{_videourl}/Video/1.mp4"},
            //    new optionsValue(){name="这个视频的名字第四集",url = $"http://{_videourl}/Video/4.mp4"},
            //    new optionsValue(){name="这个视频的名字第五集",url = $"http://{_videourl}/Video/1.mp4"},
            //    new optionsValue(){name="这个视频的名字第六集",url = $"http://{_videourl}/Video/4.mp4"},
            //    new optionsValue(){name="生化危机5",url = $"http://{_videourl}/Video/5.mp4"},
            //};
            //List<Videos> list = new List<Videos>()
            //{
            //    new Videos(){name="数控教学",options= options},
            //    new Videos(){name="智能教学",options= options},
            //    new Videos(){name="制造教学",options= options},
            //    new Videos(){name="电脑教学",options= options},
            //    new Videos(){name="编程教学",options= options},
            //    new Videos(){name="梦天教学",options= options},
            //    new Videos(){name="龙鼎教学",options= options}
            //};
            return _dal.GetList();
        }

        [HttpGet]
        [Route("getcode")]
        public string GetCode()
        {
            string url = HttpContext.Current.Request["url"];
            url = HttpUtility.UrlDecode(url);
            return QRCodeCommon.CreateQRCode(url);
        }
    }

    public class Videos
    {
        public string name { get; set; }
        public List<optionsValue> options { get; set; }
    }

    public class optionsValue
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
