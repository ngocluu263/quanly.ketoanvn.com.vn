using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace ThanhLapDN.Data
{
    public class SanPhamData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CKS_SANPHAM> GetListAll(string ma)
        {
            return this.db.CKS_SANPHAMs.Where(n => ((n.SP_MA.Contains(ma) || ma == "") && n.SP_ACTIVE == 1)).OrderBy(n => n.ID).ToList();
        }
        public virtual List<CKS_SANPHAM> GetListByName(string ma)
        {
            return this.db.CKS_SANPHAMs.Where(n => (n.SP_MA.Contains(ma) || ma == "")).OrderBy(n => n.ID).ToList();
        }
        public virtual List<CKS_SANPHAM> GetListByNCC(string ma)
        {
            return this.db.CKS_SANPHAMs.Where(n => (n.NCC_MA == ma && n.SP_ACTIVE == 1)).OrderBy(n => n.SP_ORDER).ToList();
        }
        public virtual CKS_SANPHAM GetById(int id)
        {
            try
            {
                return this.db.CKS_SANPHAMs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual void Create(CKS_SANPHAM cus)
        {
            try
            {
                this.db.CKS_SANPHAMs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(CKS_SANPHAM cus)
        {
            try
            {
                CKS_SANPHAM cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(CKS_SANPHAM b)
        {
            try
            {
                db.CKS_SANPHAMs.DeleteOnSubmit(b);
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
                CKS_SANPHAM b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual bool CheckCodeExists(string ma)
        {
            var obj = this.db.CKS_SANPHAMs.Where(n => (n.SP_MA == ma)).ToList();
            if (obj.Count > 0)
            {
                return false;
            }
            else return true;
        }
    }
}