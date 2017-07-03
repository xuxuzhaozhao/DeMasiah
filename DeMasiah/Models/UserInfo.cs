using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeMasiah.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
    }
}