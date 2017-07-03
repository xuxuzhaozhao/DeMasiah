using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeMasiah.Models
{
    public class Video
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 视频分类
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 视频的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 视频地址
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        /// 二维码地址
        /// </summary>
        public string QrCodeUrl { get; set; }

        public int Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public int Updater { get; set; }
        public DateTime UpDateTime { get; set; }
        public string Note { get; set; }
        
    }
}