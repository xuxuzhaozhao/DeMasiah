using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeMasiah.Models
{
    public class VideosVm
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<Models.VideoVm> options { get; set; }
    }
}