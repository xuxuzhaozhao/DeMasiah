using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DeMasiah.Models
{
    public class XXDbContext:DbContext
    {
        public XXDbContext() : base("DeMasiah") { }

        public DbSet<Video> Videos { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<VideoType> VideoTypes { get; set; }
    }
}