using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace ThanhLapDN.Data
{
    public class NhaCungCapData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CKS_NHACUNGCAP> GetListAll(string ma)
        {
            return this.db.CKS_NHACUNGCAPs.Where(n => (n.NCC_MA.Contains(ma) || ma == "")).OrderBy(n => n.NCC_ORDER).ToList();
        }
        public virtual List<CKS_NHACUNGCAP> GetListByName(string ma)
        {
            return this.db.CKS_NHACUNGCAPs.Where(n => ((n.NCC_MA.Contains(ma) || ma == "") && n.NCC_ACTIVE == 1)).OrderBy(n => n.NCC_ORDER).ToList();
        }
        public virtual List<CKS_NHACUNGCAP> GetListByType(string ma, int type)
        {
            return this.db.CKS_NHACUNGCAPs.Where(n => ((n.NCC_MA.Contains(ma) || ma == "") && n.NCC_TYPE == type && n.NCC_ACTIVE == 1)).OrderBy(n => n.NCC_ORDER).ToList();
        }
        public virtual CKS_NHACUNGCAP GetById(int id)
        {
            try
            {
                return this.db.CKS_NHACUNGCAPs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual void Create(CKS_NHACUNGCAP cus)
        {
            try
            {
                this.db.CKS_NHACUNGCAPs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(CKS_NHACUNGCAP cus)
        {
            try
            {
                CKS_NHACUNGCAP cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(CKS_NHACUNGCAP b)
        {
            try
            {
                db.CKS_NHACUNGCAPs.DeleteOnSubmit(b);
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
                CKS_NHACUNGCAP b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual bool CheckCodeExists(string ma)
        {
            var obj = this.db.CKS_NHACUNGCAPs.Where(n => (n.NCC_MA == ma)).ToList();
            if (obj.Count > 0)
            {
                return false;
            }
            else return true;
        }
    }
}