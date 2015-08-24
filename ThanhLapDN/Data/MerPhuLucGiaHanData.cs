using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace ThanhLapDN.Data
{
    public class MerPhuLucGiaHanData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<MER_PHULUC_GIAHAN> GetListAll()
        {
            return this.db.MER_PHULUC_GIAHANs.OrderBy(n => n.MER_DATE).ToList();
        }
        public virtual MER_PHULUC_GIAHAN GetById(int id)
        {
            try
            {
                return this.db.MER_PHULUC_GIAHANs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual bool GetByUser(int idUser)
        {
            try
            {
                var obj = db.MER_PHULUC_GIAHANs.Single(u => u.USER_ID == idUser);
                if (obj != null)
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
        public virtual void Create(MER_PHULUC_GIAHAN cus)
        {
            try
            {
                this.db.MER_PHULUC_GIAHANs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(MER_PHULUC_GIAHAN cus)
        {
            try
            {
                MER_PHULUC_GIAHAN cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(MER_PHULUC_GIAHAN b)
        {
            try
            {
                db.MER_PHULUC_GIAHANs.DeleteOnSubmit(b);
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
                MER_PHULUC_GIAHAN b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}