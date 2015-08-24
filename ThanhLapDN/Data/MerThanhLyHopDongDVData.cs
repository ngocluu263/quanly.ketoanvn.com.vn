using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace ThanhLapDN.Data
{
    public class MerThanhLyHopDongDVData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<MER_THANHLY_HOPDONG> GetListAll()
        {
            return this.db.MER_THANHLY_HOPDONGs.OrderBy(n => n.MER_DATE).ToList();
        }
        public virtual MER_THANHLY_HOPDONG GetById(int id)
        {
            try
            {
                return this.db.MER_THANHLY_HOPDONGs.Single(u => u.ID == id);
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
                var obj = db.MER_THANHLY_HOPDONGs.Single(u => u.USER_ID == idUser);
                if (obj != null)
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
        public virtual void Create(MER_THANHLY_HOPDONG cus)
        {
            try
            {
                this.db.MER_THANHLY_HOPDONGs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(MER_THANHLY_HOPDONG cus)
        {
            try
            {
                MER_THANHLY_HOPDONG cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(MER_THANHLY_HOPDONG b)
        {
            try
            {
                db.MER_THANHLY_HOPDONGs.DeleteOnSubmit(b);
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
                MER_THANHLY_HOPDONG b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}