using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeMasiah.DAL
{
    public class UserInfoDAL
    {
        #region 基本的CRUD
        public int AddUser(Models.UserInfo model)
        {
            using(var db=new Models.XXDbContext())
            {
                db.UserInfos.Add(model);
                return db.SaveChanges();
            }
        }

        public int Modified(Models.UserInfo model)
        {
            using(var db=new Models.XXDbContext())
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
        #endregion

        public Models.UserInfo Login(string LoginName,string Password)
        {
            using(var db=new Models.XXDbContext())
            {
                return db.UserInfos.Where(t => t.LoginName == LoginName && t.Password == Password).Select(t => t).SingleOrDefault();
            }
        }
    }
}