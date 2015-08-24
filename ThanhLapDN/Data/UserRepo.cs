using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.Data
{
    public class UserRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();


        public virtual USER GetById(int id)
        {
            try
            {
                return this.db.USERs.Single(u => u.USER_ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<USER> GetAll()
        {
            return this.db.USERs.ToList();
        }
        public virtual List<USER> GetAllMaCC()
        {
            return this.db.USERs.Where(n => n.USER_MACC != null && n.USER_MACC != "").ToList();
        }
        public virtual List<USER> GetInfoUserByMaCC(string _maCC, int _group)
        {
            return this.db.USERs.Where(n => n.USER_MACC.Contains(_maCC) &&
                ((n.GROUP_ID == _group || 0 == _group)
                && n.GROUP_ID != 1 && n.GROUP_ID != 2)).ToList();
        }
        public virtual List<USER> GetByGroup(int _group)
        {
            return this.db.USERs.Where(n => n.GROUP_ID == _group).ToList();
        }
        public virtual List<USER> GetByGroupBangLuong(int _group)
        {
            return this.db.USERs.Where(n => (n.GROUP_ID == _group || 0 == _group)
                && n.GROUP_ID != 1 && n.GROUP_ID != 2).ToList();
        }
        public virtual void Create(USER user)
        {
            try
            {
                this.db.USERs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(USER user)
        {
            try
            {
                USER userOld = this.GetById(user.USER_ID);
                userOld = user;
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual void Remove(int id)
        {
            try
            {
                USER user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(USER user)
        {
            try
            {
                db.USERs.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(int id)
        {
            try
            {
                USER user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(USER user)
        {
            try
            {
                //user.IsDelete = true;
                db.SubmitChanges();
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}