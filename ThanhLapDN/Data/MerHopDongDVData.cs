using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace ThanhLapDN.Data
{
    public class MerHopDongDVData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<MER_HOPDONG_DV> GetListAll()
        {
            return this.db.MER_HOPDONG_DVs.OrderBy(n => n.MER_DATE).ToList();
        }
        public virtual MER_HOPDONG_DV GetById(int id)
        {
            try
            {
                return this.db.MER_HOPDONG_DVs.Single(u => u.ID == id);
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
                var obj = db.MER_HOPDONG_DVs.Single(u => u.USER_ID == idUser);
                if (obj != null)
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
        public virtual void Create(MER_HOPDONG_DV cus)
        {
            try
            {
                this.db.MER_HOPDONG_DVs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(MER_HOPDONG_DV cus)
        {
            try
            {
                MER_HOPDONG_DV cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(MER_HOPDONG_DV b)
        {
            try
            {
                db.MER_HOPDONG_DVs.DeleteOnSubmit(b);
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
                MER_HOPDONG_DV b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}