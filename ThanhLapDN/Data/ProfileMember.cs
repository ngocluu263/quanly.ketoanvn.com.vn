using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.Data
{
    public class ProfileMember
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<PROFILE_MEMBER> GetListByName(string name)
        {
            return this.db.PROFILE_MEMBERs.Where(n => (n.MEM_FULLNAME.Contains(name) || name == "")).OrderBy(n => n.MEM_FULLNAME).ToList();
        }
        public virtual PROFILE_MEMBER GetById(int id)
        {
            try
            {
                return this.db.PROFILE_MEMBERs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual void Create(PROFILE_MEMBER cus)
        {
            try
            {
                this.db.PROFILE_MEMBERs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(PROFILE_MEMBER cus)
        {
            try
            {
                PROFILE_MEMBER cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(PROFILE_MEMBER b)
        {
            try
            {
                db.PROFILE_MEMBERs.DeleteOnSubmit(b);
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
                PROFILE_MEMBER b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual List<PROFILE_MEMBER> ListMember()
        {
            var list = db.PROFILE_MEMBERs.ToList();
            return list;
        }
    }
}