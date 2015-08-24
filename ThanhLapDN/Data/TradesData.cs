using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace ThanhLapDN.Data
{
    public class TradesData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<PROFILE_TRADE> GetListByName(string name)
        {
            return this.db.PROFILE_TRADEs.Where(n => (n.TRAD_NAME.Contains(name) || name == "")).OrderBy(n => n.TRAD_NAME).ToList();
        }
        public virtual PROFILE_TRADE GetById(int id)
        {
            try
            {
                return this.db.PROFILE_TRADEs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual void Create(PROFILE_TRADE cus)
        {
            try
            {
                this.db.PROFILE_TRADEs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(PROFILE_TRADE cus)
        {
            try
            {
                PROFILE_TRADE cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(PROFILE_TRADE b)
        {
            try
            {
                db.PROFILE_TRADEs.DeleteOnSubmit(b);
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
                PROFILE_TRADE b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}