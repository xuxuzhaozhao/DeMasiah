using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeMasiah.Models
{
    public class VideoVm
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string videourl { get; set; }
        public string qrcodeurl { get; set; }
    }
}