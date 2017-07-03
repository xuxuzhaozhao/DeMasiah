using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeMasiah.Models;
using DeMasiah.Common;

namespace DeMasiah.DAL
{
    public class VideoDAL
    {
        #region 视频分类的CRUD
        public int AddVideoType(Models.VideoType model)
        {
            using (var db = new Models.XXDbContext())
            {
                db.VideoTypes.Add(model);
                return db.SaveChanges();
            }
        }

        public int ModifiedVideoType(Models.VideoType model)
        {
            using (var db = new Models.XXDbContext())
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public int DeleteVideoType(int id)
        {
            using (var db = new Models.XXDbContext())
            {
                var model = db.VideoTypes.Where(t => t.Id == id).Select(t => t).SingleOrDefault();
                db.VideoTypes.Remove(model);
                return db.SaveChanges();
            }
        }
        #endregion

        /// <summary>
        /// 将视频分类往上顶
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddSort(int id)
        {
            using(var db=new Models.XXDbContext())
            {
                var model = db.VideoTypes.Where(t => t.Id == id).Select(t => t).SingleOrDefault();
                model.Sort = model.Sort + 1;
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取视频分类的全部
        /// </summary>
        /// <returns></returns>
        public List<Models.VideoType> GetVideoTypes()
        {
            using(var db=new Models.XXDbContext())
            {
                var list = from a in db.VideoTypes
                           orderby a.Sort descending
                           select a;
                return list.ToList();
            }
        }

        /// <summary>
        /// 根据视频分类获取该类下面所有的视频
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Models.Video> GetVideoList(int type)
        {
            using(var db =new Models.XXDbContext())
            {
                var list = from v in db.Videos
                           where v.IsDelete == false && v.Type == type
                           select v;
                return list.ToList();
            }
        }

        #region 获取Video的所有选项列表
        public List<Models.VideosVm> GetList()
        {
            using (var db = new Models.XXDbContext())
            {
                var l = from a in db.VideoTypes
                           orderby a.Sort descending
                           select new Models.VideosVm()
                           {
                               Id = a.Id,
                               name = a.Name
                           };
                var list = GetMore(l.ToList(),db);
                return list;
            }
        }

        private List<Models.VideosVm> GetMore(List<VideosVm> list, XXDbContext db)
        {
            var result = new List<Models.VideosVm>();
            foreach (var l in list)
            {
                var tmp = (from v in db.Videos
                           where v.Type == v.Type && v.IsDelete == false
                           select new Models.VideoVm()
                           {
                               Id = v.Id,
                               name = v.Name,
                               qrcodeurl = v.QrCodeUrl,
                               videourl = v.VideoUrl
                           }).ToList();
                l.options = tmp;
                result.Add(l);
            }
            return result;
        }
        #endregion

        #region Video的CRUD
        public int AddVideo(Models.Video model,string url)
        {
            using(var db=new Models.XXDbContext())
            {
                db.Videos.Add(model);
                db.SaveChanges();

                var qrcodeurl = $"http://{url}/Home/Video/{model.Id}";
                var qrCodeUrl = QRCodeCommon.CreateQRCode(qrcodeurl);
                model.QrCodeUrl = $"http://{url}{qrCodeUrl}";
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
        public int ModifiedVideo(Models.Video model)
        {
            using(var db=new Models.XXDbContext())
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
        public int DeleteVideo(int id)
        {
            using(var db=new Models.XXDbContext())
            {
                var model = db.Videos.Where(t => t.Id == id).Select(t => t).SingleOrDefault();
                model.IsDelete = true;
                model.UpDateTime = DateTime.Now;
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public Models.VideoVm GetVideoById(int id)
        {
            using(var db=new Models.XXDbContext())
            {
                var model = db.Videos.Where(t => t.Id == id).Select(t => t).SingleOrDefault();
                var video = new VideoVm()
                {
                    Id = model.Id,
                    name = model.Name,
                    videourl = model.VideoUrl,
                    qrcodeurl = model.QrCodeUrl
                };
                return video;
            }
        }
        #endregion
    }
}