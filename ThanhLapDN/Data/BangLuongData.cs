using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.Data
{
    public class BangLuongData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<LUONG_DANHSACH> GetListByName(string name)
        {
            return this.db.LUONG_DANHSACHes.Where(n => (n.BL_TENNV.Contains(name) || name == "")).OrderBy(n => n.ID).ToList();
        }
        public virtual List<LUONG_DANHSACH> GetListByYear(string name, int _month, int _year)
        {
            return this.db.LUONG_DANHSACHes.Where(n => n.BL_NAM == _year
                && n.BL_THANG == _month
                && (n.BL_TENNV.Contains(name) || name == "")).OrderBy(n => n.BL_PHONGBAN).ToList();
        }
        public virtual List<LUONG_DANHSACH> GetListByNV(string name, int _month, int _year, string _userName)
        {
            return this.db.LUONG_DANHSACHes.Where(n => (n.BL_NAM == _year)
                && n.BL_THANG == _month
                && (n.BL_TENNV.Contains(name) || name == "")
                && n.BL_MANV == _userName).OrderBy(n => n.BL_PHONGBAN).ToList();
        }
        public virtual LUONG_DANHSACH GetById(int id)
        {
            try
            {
                return this.db.LUONG_DANHSACHes.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual LUONG_DANHSACH GetByUserName(string _userName)
        {
            try
            {
                return this.db.LUONG_DANHSACHes.Single(u => u.BL_MANV == _userName);
            }
            catch
            {
                return null;
            }
        }
        public virtual void Create(LUONG_DANHSACH cus)
        {
            try
            {
                this.db.LUONG_DANHSACHes.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(LUONG_DANHSACH cus)
        {
            try
            {
                LUONG_DANHSACH cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(LUONG_DANHSACH b)
        {
            try
            {
                db.LUONG_DANHSACHes.DeleteOnSubmit(b);
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
                LUONG_DANHSACH b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void RemoveByYearMonth(int _year, int _month, int _phongban)
        {
            try
            {
                var obj = db.LUONG_DANHSACHes.Where(n => n.BL_NAM == _year && n.BL_THANG == _month
                    && (n.BL_PHONGBAN == _phongban || 0 == _phongban)).ToList();
                if (obj.Count > 0)
                {
                    db.LUONG_DANHSACHes.DeleteAllOnSubmit(obj);
                    db.SubmitChanges();
                }
            }
            catch (Exception) { throw; }
        }
        public virtual bool CheckExistsYearMonth(int _year, int _month, int _phongban)
        {
            try
            {
                var obj = db.LUONG_DANHSACHes.Where(n => n.BL_NAM == _year && n.BL_THANG == _month 
                    && (n.BL_PHONGBAN == _phongban || _phongban == 0)).ToList();
                if (obj.Count > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception) { throw; }
        }
    }
}