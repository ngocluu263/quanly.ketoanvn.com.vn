using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;

namespace ThanhLapDN.Data
{
    public class UserPermissionData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual USER_PERMISSION GetById(int id)
        {
            try
            {
                return this.db.USER_PERMISSIONs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual void Create(USER_PERMISSION cus)
        {
            try
            {
                this.db.USER_PERMISSIONs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(USER_PERMISSION cus)
        {
            try
            {
                USER_PERMISSION cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(USER_PERMISSION b)
        {
            try
            {
                db.USER_PERMISSIONs.DeleteOnSubmit(b);
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
                USER_PERMISSION b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual List<USER_PERMISSION> GetByType(int _type)
        {
            try
            {
                return this.db.USER_PERMISSIONs.Where(n => n.PER_TYPE == _type).OrderBy(n => n.PER_GROUP).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}