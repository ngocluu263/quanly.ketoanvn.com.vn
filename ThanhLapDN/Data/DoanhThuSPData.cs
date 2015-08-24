using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;

namespace ThanhLapDN.Data
{
    public class DoanhThuSPData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<LUONG_DOANHTHU_SP> GetListByName(string name)
        {
            return this.db.LUONG_DOANHTHU_SPs.Where(n => (n.MA_NV == name || name == "")).OrderBy(n => n.ID).ToList();
        }
        public virtual List<LUONG_DOANHTHU_SP> GetListByYear(string name, int _month, int _year)
        {
            return this.db.LUONG_DOANHTHU_SPs.Where(n => n.NAM == _year && n.THANG == _month
                && (n.MA_NV == name || name == "")).OrderBy(n => n.ID).ToList();
        }
        public virtual List<LUONG_DOANHTHU_SP> GetListByMaNV(string _manv, int _month, int _year)
        {
            return this.db.LUONG_DOANHTHU_SPs.Where(n => n.NAM == _year && n.THANG == _month
                && n.MA_NV == _manv).OrderBy(n => n.ID).ToList();
        }
        public virtual LUONG_DOANHTHU_SP GetById(int id)
        {
            try
            {
                return this.db.LUONG_DOANHTHU_SPs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }

        public virtual void Create(LUONG_DOANHTHU_SP cus)
        {
            try
            {
                this.db.LUONG_DOANHTHU_SPs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(LUONG_DOANHTHU_SP cus)
        {
            try
            {
                LUONG_DOANHTHU_SP cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(LUONG_DOANHTHU_SP b)
        {
            try
            {
                db.LUONG_DOANHTHU_SPs.DeleteOnSubmit(b);
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
                LUONG_DOANHTHU_SP b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void RemoveByMaNV(int _year, int _month, string _manv)
        {
            try
            {
                var obj = db.LUONG_DOANHTHU_SPs.Where(n => n.NAM == _year && n.THANG == _month
                    && n.MA_NV == _manv).ToList();
                if (obj.Count > 0)
                {
                    db.LUONG_DOANHTHU_SPs.DeleteAllOnSubmit(obj);
                    db.SubmitChanges();
                }
            }
            catch (Exception) { throw; }
        }
        public virtual void RemoveByPhongBan(int _year, int _month, int _phongban)
        {
            try
            {
                var obj = db.LUONG_DOANHTHU_SPs.Where(n => n.NAM == _year && n.THANG == _month
                    && n.PHONGBAN == _phongban).ToList();
                if (obj.Count > 0)
                {
                    db.LUONG_DOANHTHU_SPs.DeleteAllOnSubmit(obj);
                    db.SubmitChanges();
                }
            }
            catch (Exception) { throw; }
        }
        public virtual bool CheckExistsYearMonth(int _year, int _month, string _manv)
        {
            try
            {
                var obj = db.LUONG_DOANHTHU_SPs.Where(n => n.NAM == _year && n.THANG == _month
                    && n.MA_NV == _manv).ToList();
                if (obj.Count > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception) { throw; }
        }

        public virtual decimal getDoanhThu(int _year, int _month, string _manv)
        {
            decimal temp = 0;
            var obj = db.LUONG_DOANHTHU_SPs.Where(n => n.NAM == _year && n.THANG == _month
                    && n.MA_NV == _manv).Sum(n => n.DOANH_THU);
            if (obj > 0)
                temp = Utils.CDecDef(obj);
            return temp;
        }
        public virtual decimal getLuongSP(int _year, int _month, string _manv)
        {
            decimal temp = 0;
            var obj = db.LUONG_DOANHTHU_SPs.Where(n => n.NAM == _year && n.THANG == _month
                    && n.MA_NV == _manv).Sum(n => n.LUONG_SP);
            if (obj > 0)
                temp = Utils.CDecDef(obj);
            return temp;
        }
        public virtual decimal getLuongDATV(int _year, int _month, string _manv)
        {
            decimal temp = 0;
            var obj = db.LUONG_DOANHTHU_SPs.Where(n => n.NAM == _year && n.THANG == _month
                    && n.MA_NV == _manv).Sum(n => n.LUONG_DUAN_TV);
            if (obj > 0)
                temp = Utils.CDecDef(obj);
            return temp;
        }
    }
}