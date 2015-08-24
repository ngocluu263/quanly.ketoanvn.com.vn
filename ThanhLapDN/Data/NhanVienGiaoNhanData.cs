using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;
using System.Data;

namespace ThanhLapDN.Data
{
    public class NhanVienGiaoNhanData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<NV_GIAONHAN> GetListByName(string name)
        {
            return this.db.NV_GIAONHANs.Where(n => (n.USER_NAME.Contains(name) || name == "")).OrderBy(n => n.ID).ToList();
        }
        public virtual IQueryable GetList1()
        {
            var obj = (from a in db.NV_GIAONHANs
                       select new
                       {
                           a.USER_ID,
                           a.USER_NAME,
                           a.PROP_PARENT_ID
                       }).Distinct();
            return obj;
        }
        public virtual NV_GIAONHAN GetById(int id)
        {
            try
            {
                return this.db.NV_GIAONHANs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual void Create(NV_GIAONHAN cus)
        {
            try
            {
                this.db.NV_GIAONHANs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(NV_GIAONHAN cus)
        {
            try
            {
                NV_GIAONHAN cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(NV_GIAONHAN b)
        {
            try
            {
                db.NV_GIAONHANs.DeleteOnSubmit(b);
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
                NV_GIAONHAN b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual List<NV_GIAONHAN> GetListArea(int _type)
        {
            return this.db.NV_GIAONHANs.Where(n => _type == 1 ?
                n.PROP_ID != null : n.PROP_ID_OTHER != null).OrderBy(n => n.ID).ToList();
        }
    }
}